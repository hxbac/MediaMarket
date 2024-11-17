'use client';

import { Tabs } from 'antd';
import Video from './_components/video';
import Image from './_components/image';
import { ForwardRefExoticComponent, RefAttributes, useState } from 'react';
import { AntdIconProps } from '@ant-design/icons/lib/components/AntdIcon';
import Actions from './_components/actions';
import { PictureOutlined, YoutubeOutlined } from '@ant-design/icons';
import { SearchProductParams } from '@/interfaces/products';
import { SearchProductContextProvider } from '../products/_context/SearchProductContext';

interface TabType {
  icon: ForwardRefExoticComponent<Omit<AntdIconProps, "ref"> & RefAttributes<HTMLSpanElement>>,
  label: string,
  component: () => JSX.Element
}

const tabs : TabType[] = [
  {
    icon: YoutubeOutlined,
    label: 'Video',
    component: Video
  },
  {
    icon: PictureOutlined,
    label: 'Hình Ảnh',
    component: Image
  }
];

const operations = <Actions />;

export default function Page() {
  const [searchParams, setSearchParams] = useState<SearchProductParams>({
    name: ''
  });

  return (
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl px-4 py-8 mx-auto lg:gap-16 xl:gap-24 lg:py-24 lg:px-6">
        <h1 className="text-center uppercase text-xl font-bold mb-4">Lịch sử mua hàng</h1>
        <SearchProductContextProvider initial={{
          value: searchParams,
          setValue: setSearchParams
         }}>
          <Tabs
            defaultActiveKey="1"
            items={tabs.map((item, i) => {
              const id = String(i + 1);

              return {
                key: id,
                label: item.label,
                children: <item.component />,
                icon: <item.icon />,
              };
            })}
            tabBarExtraContent={operations}
          />
         </SearchProductContextProvider>
      </div>
    </section>
  );
}
