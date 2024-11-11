"use client";

import React, { useRef, useState } from "react";
import { InboxOutlined } from '@ant-design/icons';
import type { UploadFile, UploadProps } from 'antd';
import { Modal, message, Upload, Empty, Typography, Button, TimePicker } from "antd";
import { useProductContext } from "../../_context/ProductContext";
import { UploadChangeParam } from "antd/es/upload";

const { Dragger } = Upload;

const props: UploadProps = {
  name: 'file',
  multiple: false,
  action: process.env.NEXT_PUBLIC_API_UPLOAD_SINGLE_FILE,
  maxCount: 1
};

function formatSeconds(seconds: number): string {
  const hours = Math.floor(seconds / 3600);
  const minutes = Math.floor((seconds % 3600) / 60);
  const secs = Math.floor(seconds % 60);

  const formattedHours = hours.toString().padStart(2, '0');
  const formattedMinutes = minutes.toString().padStart(2, '0');
  const formattedSeconds = secs.toString().padStart(2, '0');

  return `${formattedHours}:${formattedMinutes}:${formattedSeconds}`;
}

export default function UploadVideo() {
  const videoOriginalElement = useRef(null);
  const [videoDuration, setVideoDuration] = useState<number | null>(null);
  const [fileUpload, setFileUpload] = useState<UploadFile|null>(null);
  const { value, setValue } = useProductContext();
  const [isModalOpen, setIsModalOpen] = useState(false);

  const onChangeFileEvent = (info: UploadChangeParam<UploadFile>) => {
    const { status } = info.file;
    if (status !== 'uploading') {
      setFileUpload(info.file);
    }
    if (status === 'done') {
      message.success(`${info.file.name} file uploaded successfully.`);
    } else if (status === 'error') {
      message.error(`${info.file.name} file upload failed.`);
    }
  }

  const showModal = () => {
    setIsModalOpen(true);
  };

  const handleOk = () => {
    if (fileUpload !== null) {
      setValue({
        ...value,
        originalFiles: [
          {
            id: null,
            url: fileUpload.response.data[0].url
          }
        ]
      });
    }
    setIsModalOpen(false);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  const handleMetadataLoaded = () => {
    if (videoOriginalElement.current) {
      setVideoDuration(videoOriginalElement.current['duration']);
    }
  };

  return (
    <div className="">
      <div className="flex items-center justify-between">
        <h2 className="text-2xl font-bold mt-8 mb-4">Video</h2>
        <div
          className="py-1 px-4 border rounded-md select-none cursor-pointer"
          onClick={showModal}
        >
          Click to Upload
        </div>
      </div>
      <div>
        {
          value.originalFiles.length > 0 ?
          (
            <video controls width="100%" ref={videoOriginalElement} onLoadedMetadata={handleMetadataLoaded}>
              <source src={value.originalFiles[0].url} type="video/mp4" />
              Sorry, your browser doesn&apos;t support embedded videos.
            </video>
          ) :
          (
            <Empty
              image="/icons/empty.svg"
              imageStyle={{ height: 60 }}
              className="flex flex-col items-center justify-center border py-20"
              description={
                <Typography.Text>
                  Customize <a href="#API">Description</a>
                </Typography.Text>
              }
            >
              <Button type="primary">Create Now</Button>
            </Empty>
          )
        }
      </div>
      <div>
        <h2 className="text-2xl font-bold mt-8 mb-4">Preview</h2>
        {
          videoDuration !== null ?
          (
            <div>
              <p className="mb-4">Giới hạn thời lượng có thể phát preview là từ {formatSeconds(0)} đến {formatSeconds(videoDuration)}</p>
              <TimePicker.RangePicker  />
            </div>
          ) :
          (<></>)
        }
      </div>
      <Modal
        title="Đăng tải tệp tin"
        open={isModalOpen}
        onOk={handleOk}
        onCancel={handleCancel}
      >
        <Dragger 
          {...props}
          onChange={onChangeFileEvent}
        >
          <p className="ant-upload-drag-icon">
            <InboxOutlined />
          </p>
          <p className="ant-upload-text">
            Click or drag file to this area to upload
          </p>
          <p className="ant-upload-hint">
            Support for a single or bulk upload. Strictly prohibited from
            uploading company data or other banned files.
          </p>
        </Dragger>
      </Modal>
    </div>
  );
}
