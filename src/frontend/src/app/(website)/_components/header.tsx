'use client';

import { useAppDispatch, useAppSelector } from "@/store/hooks";
import { login } from "@/store/userSlice";
import authService from '@/services/authService.js';
import Link from "next/link";
import { useEffect } from "react";
import { toast } from "react-toastify";
import { Dropdown, MenuProps } from "antd";
import { CaretDownOutlined, ContainerOutlined, CreditCardOutlined, ProductOutlined, SettingOutlined, UserOutlined } from "@ant-design/icons";
import Image from "next/image";

const items: MenuProps['items'] = [
  {
    key: '1',
    icon: <UserOutlined />,
    label: (
      <Link href="/account">Tài khoản</Link>
    ),
  },
  {
    type: 'divider',
  },
  {
    key: '2',
    icon: <ProductOutlined />,
    label: (
      <Link href="/dashboard/products">Quản lý sản phẩm</Link>
    ),
  },
  {
    key: '3',
    type: 'group',
    label: "Đơn hàng",
    children: [
      {
        key: '3-1',
        icon: <ContainerOutlined />,
        label: (
          <Link href="/dashboard/purchases">Đơn mua</Link>
        ),
      },
      {
        key: '3-2',
        icon: <CreditCardOutlined />,
        label: (
          <Link href="/dashboard/orders">Đơn bán</Link>
        ),
      },
    ],
  },
  {
    key: '4',
    icon: <ProductOutlined />,
    label: (
      <Link href="/dashboard">Quản lý sản phẩm</Link>
    ),
  },
  {
    type: 'divider',
  },
  {
    key: '5',
    label: 'Settings',
    icon: <SettingOutlined />,
    extra: '⌘S',
  },
];

export default function Header() {
  const isLoggedIn = useAppSelector(state => state.user.isLoggedIn);
  const user = useAppSelector(state => state.user.user);

  const dispatch = useAppDispatch();

  useEffect(() => {
    const getProfile = async () => {
      try {
        const result = await authService.getProfile();
        if (result.succeeded) {
          dispatch(login(result.data));
        } else {
          throw new Error(result.message);
        }
      } catch (error: unknown) {
        const errorMessage = error instanceof Error ? error.message : 'An unexpected error occurred';
        toast.error(errorMessage);
      }
    }

    const token = localStorage.getItem('accessToken');
    if (token) {
      getProfile();
    }
  }, [dispatch]);

  return (
    <header className="fixed w-full z-[999]">
      <nav className="bg-white border-gray-200 py-2.5">
        <div className="flex flex-wrap items-center justify-between max-w-screen-xl px-4 mx-auto">
          <a href="#" className="flex items-center">
            <span className="self-center text-xl font-semibold whitespace-nowrap">
              Landwind
            </span>
          </a>
          <div className="flex items-center lg:order-2 gap-x-3">
            {
              isLoggedIn && user !== null ?
              (
                <>
                  <Dropdown menu={{ items }} className="py-2">
                    <div className="cursor-pointer flex items-center gap-x-4 min-w-48 justify-end">
                      <div>
                        <span className="block text-sm font-medium text-black text-end">{user.name}</span>
                        <span className="block text-xs font-medium text-end">{user.email}</span>
                      </div>
                      <div>
                        <Image
                          src="https://localhost:7148/storage/2024/11/bg.webp"
                          alt="abc"
                          width={36}
                          height={36}
                          className="rounded-full object-cover w-9 h-9"
                          unoptimized
                        />
                      </div>
                      <div>
                        <CaretDownOutlined />
                      </div>
                    </div>
                  </Dropdown>
                </>
              ) :
              (
              <>
                <Link
                  href="/auth/register"
                  className="text-black hover:text-white border-2 border-purple-700 hover:bg-purple-700 focus:ring-4 focus:ring-purple-300 font-medium rounded-lg text-sm px-4 lg:px-5 py-2 lg:py-2 sm:mr-2 lg:mr-0 focus:outline-none transition-all"
                >
                  Đăng Ký
                </Link>
                <Link
                  href="/auth/login"
                  className="text-white bg-purple-700 border-2 border-purple-700 hover:bg-purple-800 focus:ring-4 focus:ring-purple-300 font-medium rounded-lg text-sm px-4 lg:px-5 py-2 lg:py-2 sm:mr-2 lg:mr-0 focus:outline-none transition-all"
                >
                  Đăng Nhập
                </Link>
              </>
              )
            }
          </div>
          <div
            className="items-center justify-between hidden w-full lg:flex lg:w-auto lg:order-1"
            id="mobile-menu-2"
          >
            <ul className="flex flex-col mt-4 font-medium lg:flex-row lg:space-x-8 lg:mt-0">
              <li>
                <a
                  href="#"
                  className="block py-2 pl-3 pr-4 text-white bg-purple-700 rounded lg:bg-transparent lg:text-purple-700 lg:p-0"
                  aria-current="page"
                >
                  Home
                </a>
              </li>
              <li>
                <a
                  href="#"
                  className="block py-2 pl-3 pr-4 text-gray-700 border-b border-gray-100 hover:bg-gray-50 lg:hover:bg-transparent lg:border-0 lg:hover:text-purple-700 lg:p-0"
                >
                  Company
                </a>
              </li>
              <li>
                <a
                  href="#"
                  className="block py-2 pl-3 pr-4 text-gray-700 border-b border-gray-100 hover:bg-gray-50 lg:hover:bg-transparent lg:border-0 lg:hover:text-purple-700 lg:p-0"
                >
                  Marketplace
                </a>
              </li>
              <li>
                <a
                  href="#"
                  className="block py-2 pl-3 pr-4 text-gray-700 border-b border-gray-100 hover:bg-gray-50 lg:hover:bg-transparent lg:border-0 lg:hover:text-purple-700 lg:p-0"
                >
                  Features
                </a>
              </li>
              <li>
                <a
                  href="#"
                  className="block py-2 pl-3 pr-4 text-gray-700 border-b border-gray-100 hover:bg-gray-50 lg:hover:bg-transparent lg:border-0 lg:hover:text-purple-700 lg:p-0"
                >
                  Team
                </a>
              </li>
              <li>
                <a
                  href="#"
                  className="block py-2 pl-3 pr-4 text-gray-700 border-b border-gray-100 hover:bg-gray-50 lg:hover:bg-transparent lg:border-0 lg:hover:text-purple-700 lg:p-0"
                >
                  Contact
                </a>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </header>
  );
}
