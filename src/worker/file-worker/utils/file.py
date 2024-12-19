import os
import requests

def download_file(url, folder_path, file_name=None):
    os.makedirs(folder_path, exist_ok=True)

    if not file_name:
        file_name = url.split('/')[-1]

    file_path = os.path.join(folder_path, file_name)

    response = requests.get(url, stream=True, verify=False)
    response.raise_for_status()

    with open(file_path, 'wb') as file:
        for chunk in response.iter_content(chunk_size=8192):
            file.write(chunk)

    return file_path
