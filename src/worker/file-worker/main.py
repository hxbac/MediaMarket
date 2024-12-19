from utils.file import download_file
from utils.picpurify import check_image_content
from dotenv import load_dotenv
import pika
import os
import json
import requests

load_dotenv()

connection = pika.BlockingConnection(pika.ConnectionParameters(
    host=os.getenv('RABBITMQ_HOST'),
    port=os.getenv('RABBITMQ_PORT'),
    credentials=pika.PlainCredentials(os.getenv('RABBITMQ_USERNAME'), os.getenv('RABBITMQ_PASSWORD'))
))
channel = connection.channel()

queue_name = os.getenv('QUEUE_NAME')
channel.queue_declare(queue=queue_name)

folder_path = "./temp"
def handle_queue(ch, method, properties, body):
    message = body.decode('utf-8')
    payload = json.loads(message)

    content_status = 3
    for url in payload['ImagesUrl']:
        path = download_file(url, folder_path)
        result = check_image_content(path)
        if (result != 3):
            content_status = result
            break

    headers = {
        "Content-Type": "application/json"
    }
    data = {
        "id": payload['Id'],
        "Status": content_status
    }
    response = requests.put("https://localhost:7148/api/v1/worker/products/update-content-status", headers=headers, json=data, verify=False)

    ch.basic_ack(delivery_tag=method.delivery_tag)


channel.basic_consume(queue=queue_name, on_message_callback=handle_queue)

print(f"Waiting for messages in {queue_name}")
channel.start_consuming()
