"use client";

import React, { useEffect, useState } from "react";
import { InboxOutlined } from '@ant-design/icons';
import type { UploadFile, UploadProps } from 'antd';
import { Modal, message, Upload, Empty, Typography, Button } from "antd";
import { useProductContext } from "../../_context/ProductContext";
import { UploadChangeParam } from "antd/es/upload";
import { FileInfo } from "@/interfaces/products";

const { Dragger } = Upload;

const props: UploadProps = {
  name: 'file',
  multiple: true,
  action: process.env.NEXT_PUBLIC_API_UPLOAD_SINGLE_FILE
};

export default function UploadImage() {
  const [filesUpload, setFilesUpload] = useState<UploadFile[]>([]);
  const { value, setValue } = useProductContext();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [imageGroups, setImageGroups] = useState<FileInfo[][]>([]);
  const [previewImages, setPreviewImages] = useState<string[]>([]);

  useEffect(() => {
    const groups: FileInfo[][] = [[], [], []];

    value.originalFiles.forEach((item, index) => {
      const indexGroup = index % 3;
      groups[indexGroup].push(item);
    });

    setImageGroups(groups);
  }, [value.originalFiles])

  const onChangeFileEvent = (info: UploadChangeParam<UploadFile>) => {
    const { status } = info.file;
    if (status !== 'uploading') {
      setFilesUpload(info.fileList);
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
    if (filesUpload !== null) {
      const newOriginalFiles = filesUpload.map(item => {
        return {
          id: '',
          url: item.response.data[0].url
        }
      })
      setValue({
        ...value,
        originalFiles: newOriginalFiles
      });
    }
    setIsModalOpen(false);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  const togglePreviewImage = (url: string) => {
    const index = previewImages.findIndex(value => value === url);
    if (index === -1) {
      setPreviewImages([...previewImages, url]);
    } else {
      previewImages.splice(index, 1);
      setPreviewImages([...previewImages]);
    }
  }

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
            
            <div className="grid grid-cols-3 gap-x-6">
              {
                imageGroups.map((group, index) => (
                  <div key={index} className="flex flex-col gap-y-6">
                    {
                      group.map(image => (
                        <div key={image.id} onClick={() => togglePreviewImage(image.url)} className="rounded-xl cursor-pointer overflow-hidden relative">
                          <img src={image.url} alt={image.url} className="w-full" />
                          <div className="absolute top-4 left-4 bg-white p-1 rounded">
                            {
                              previewImages.includes(image.url) ?
                              (<svg width="24px" height="24px" viewBox="0 0 24 24" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink"><g stroke="none" strokeWidth="1" fill="#9333ea" fillRule="evenodd"><g fill="#9333ea" fillRule="nonzero"><path d="M18.25,3 C19.7687831,3 21,4.23121694 21,5.75 L21,18.25 C21,19.7687831 19.7687831,21 18.25,21 L5.75,21 C4.23121694,21 3,19.7687831 3,18.25 L3,5.75 C3,4.23121694 4.23121694,3 5.75,3 L18.25,3 Z M18.25,4.5 L5.75,4.5 C5.05964406,4.5 4.5,5.05964406 4.5,5.75 L4.5,18.25 C4.5,18.9403559 5.05964406,19.5 5.75,19.5 L18.25,19.5 C18.9403559,19.5 19.5,18.9403559 19.5,18.25 L19.5,5.75 C19.5,5.05964406 18.9403559,4.5 18.25,4.5 Z M10,14.4393398 L16.4696699,7.96966991 C16.7625631,7.6767767 17.2374369,7.6767767 17.5303301,7.96966991 C17.7965966,8.23593648 17.8208027,8.65260016 17.6029482,8.94621165 L17.5303301,9.03033009 L10.5303301,16.0303301 C10.2640635,16.2965966 9.84739984,16.3208027 9.55378835,16.1029482 L9.46966991,16.0303301 L6.46966991,13.0303301 C6.1767767,12.7374369 6.1767767,12.2625631 6.46966991,11.9696699 C6.73593648,11.7034034 7.15260016,11.6791973 7.44621165,11.8970518 L7.53033009,11.9696699 L10,14.4393398 L16.4696699,7.96966991 L10,14.4393398 Z"></path></g></g></svg>) :
                              (<svg width="24" height="24" viewBox="0 0 24 24" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink"><g stroke="none" strokeWidth="1" fill="none" fillRule="evenodd"><g fill="#212121" fillRule="nonzero"><path d="M5.75,3 L18.25,3 C19.7687831,3 21,4.23121694 21,5.75 L21,18.25 C21,19.7687831 19.7687831,21 18.25,21 L5.75,21 C4.23121694,21 3,19.7687831 3,18.25 L3,5.75 C3,4.23121694 4.23121694,3 5.75,3 Z M5.75,4.5 C5.05964406,4.5 4.5,5.05964406 4.5,5.75 L4.5,18.25 C4.5,18.9403559 5.05964406,19.5 5.75,19.5 L18.25,19.5 C18.9403559,19.5 19.5,18.9403559 19.5,18.25 L19.5,5.75 C19.5,5.05964406 18.9403559,4.5 18.25,4.5 L5.75,4.5 Z"></path></g></g></svg>)
                            }
                          </div>
                        </div>
                      ))
                    }
                  </div>
                ))
              }
            </div>
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
