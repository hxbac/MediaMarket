import os
import requests
from moviepy.video.io.VideoFileClip import VideoFileClip

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

def clear_folder(folder_path):
    if os.path.exists(folder_path):
        for filename in os.listdir(folder_path):
            file_path = os.path.join(folder_path, filename)
            try:
                if os.path.isfile(file_path) or os.path.islink(file_path):
                    os.unlink(file_path)
                elif os.path.isdir(file_path):
                    shutil.rmtree(file_path)
            except Exception as e:
                print(f"Failed to delete {file_path}. Reason: {e}")
    else:
        print(f"The folder {folder_path} does not exist.")

def cut_video(input_path, output_path, start_time, end_time):
    try:
        video = VideoFileClip(input_path)

        clipped = video.subclipped(start_time, end_time)

        clipped.write_videofile(output_path, codec="libx264", audio_codec="aac")

        video.close()
    except Exception as e:
        print(f"Đã xảy ra lỗi: {e}")

def upload_file(url, file_path):
    file_name = file_path.rsplit('/', 1)[-1]
    with open(file_path, 'rb') as file:
        files = {'file': (file_name, file, 'application/octet-stream')}
        response = requests.post(url, files=files, verify=False)

    if response.status_code == 200:
        try:
            result = response.json()
            if result.get('succeeded'):
                return result['data'][0]['url']
            else:
                print(f"Error: {result.get('message', 'No message provided.')}")
                return None
        except ValueError:
            print("Invalid response format from server. Response is not valid JSON.")
            return None