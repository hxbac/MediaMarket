from utils.file import download_file, clear_folder, cut_video, upload_file, create_zip_file
from utils.picpurify import check_image_content
from utils.helpers import time_to_seconds
from dotenv import load_dotenv
import pika
import os
import json
import requests

load_dotenv()

backend_url = os.getenv('BACKEND_URL')

connection = pika.BlockingConnection(pika.ConnectionParameters(
    host=os.getenv('RABBITMQ_HOST'),
    port=os.getenv('RABBITMQ_PORT'),
    credentials=pika.PlainCredentials(os.getenv('RABBITMQ_USERNAME'), os.getenv('RABBITMQ_PASSWORD'))
))
channel = connection.channel()

queue_name = os.getenv('QUEUE_NAME')
channel.queue_declare(queue=queue_name)

folder_path = "./temp"
headers = {
    "Content-Type": "application/json"
}
def handle_queue(ch, method, properties, body):
    message = body.decode('utf-8')
    payload = json.loads(message)

    previewUrls = []
    if (payload.get('ContentType') == 'Image'):
        content_status = 3
        imagesPath = []
        for url in payload['ImagesUrl']:
            path = download_file(url, folder_path)
            imagesPath.append(path)
            # result = check_image_content(path)
            # if (result != 3):
            #     content_status = result
            #     break

        if (content_status == 3):
            compressPath = f"{folder_path}/{payload['ProductId']}-compress.zip"
            create_zip_file(imagesPath, compressPath)
            productDetailUrl = upload_file(backend_url + "/files/upload", compressPath)
            previewUrls.extend(payload['ImagesUrl'])

    else:
        path = download_file(payload['FileUrl'], folder_path)

        previewPath = f"{folder_path}/{payload['ProductId']}-preview.mp4"
        cut_video(
            path,
            previewPath,
            time_to_seconds(payload['RangePreview'][0]),
            time_to_seconds(payload['RangePreview'][1]),
        )

        videoPreviewUrl = upload_file(backend_url + "/files/upload", previewPath)
        previewUrls.append(videoPreviewUrl)

        productDetailUrl = payload['FileUrl']
        content_status = 3


    data = {
        "id": payload['ProductId'],
        "Status": content_status
    }
    response = requests.put(backend_url + "/worker/products/update-content-status", headers=headers, json=data, verify=False)

    if (content_status == 3):
        data = {
            "productDetailId": payload['ProductDetailId'],
            "fileUrl": productDetailUrl
        }
        requests.put(backend_url + "/worker/products/update-product-detail", headers=headers, json=data, verify=False)

        data = {
            "productId": payload['ProductId'],
            "previewUrls": previewUrls
        }
        requests.post(backend_url + "/worker/products/create-product-preview", headers=headers, json=data, verify=False)

    clear_folder(folder_path)
    ch.basic_ack(delivery_tag=method.delivery_tag)


channel.basic_consume(queue=queue_name, on_message_callback=handle_queue)

print(f"Waiting for messages in {queue_name}")
channel.start_consuming()
