import { AppleOutlined } from '@ant-design/icons';
import { Tabs } from 'antd';
import Video from './_components/video';
import Image from './_components/image';
import Ebook from './_components/ebook';
import { ForwardRefExoticComponent, RefAttributes } from 'react';
import { AntdIconProps } from '@ant-design/icons/lib/components/AntdIcon';
import Actions from './_components/actions';

interface TabType {
  icon: ForwardRefExoticComponent<Omit<AntdIconProps, "ref"> & RefAttributes<HTMLSpanElement>>,
  label: string,
  component: () => JSX.Element
}

export default function Page() {
  const tabs : TabType[] = [
    {
      icon: AppleOutlined,
      label: 'Video',
      component: Video
    },
    {
      icon: AppleOutlined,
      label: 'Image',
      component: Image
    },
    {
      icon: AppleOutlined,
      label: 'EBook',
      component: Ebook
    }
  ];

  const operations = <Actions />;

  return (
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl px-4 py-8 mx-auto lg:gap-16 xl:gap-24 lg:py-24 lg:px-6">
        <h1 className="text-center uppercase text-xl font-bold mb-4">Lịch sử bán hàng</h1>
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
      </div>
    </section>
  );
}
