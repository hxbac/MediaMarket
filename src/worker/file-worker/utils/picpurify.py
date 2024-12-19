import requests
import os
import json

def check_image_content(path):
    picpurify_url = 'https://www.picpurify.com/analyse/1.1'
    img_data = {'file_image': open(path, 'rb')}

    content_moderation_types = [
        "porn",
        "drug",
        "weapon",
        "gore"
    ]

    result_data = requests.post(picpurify_url,files = img_data, data = {
        "API_KEY": os.getenv('PICPURIFY_KEY'),
        "task": ",".join(f"{content_type}_moderation" for content_type in content_moderation_types)
    })
    response = json.loads(result_data.content)

    if (response['confidence_score_decision'] < 0.75):
        return 2

    if (response['final_decision'] == 'OK'):
        return 3
    return 4
