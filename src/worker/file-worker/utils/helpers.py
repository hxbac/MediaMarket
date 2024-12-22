def time_to_seconds(time_str):
    parts = list(map(int, time_str.split(":")))

    if len(parts) == 2:
        minutes, seconds = parts
        return minutes * 60 + seconds
    elif len(parts) == 3:
        hours, minutes, seconds = parts
        return hours * 3600 + minutes * 60 + seconds
    else:
        raise ValueError("Định dạng thời gian không hợp lệ.")
