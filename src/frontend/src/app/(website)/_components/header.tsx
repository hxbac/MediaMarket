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
    <header className="fixed w-full z-[999] border-b">
      <nav className="bg-white border-gray-200 py-2.5">
        <div className="flex flex-wrap items-center justify-between max-w-screen-xl px-4 mx-auto">
          <div className="flex items-center">
            <Link href={'/'} className="flex items-center mr-6">
              <span className="self-center text-xl font-semibold whitespace-nowrap">
                Media Market
              </span>
            </Link>
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
              </ul>
              <div className="ml-6 relative">
                <div className="flex px-4 py-3 rounded-md border-2 border-purple-600 overflow-hidden w-[352px] mx-auto font-[sans-serif]">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 192.904 192.904" width="16px"
                    className="fill-gray-600 mr-3 rotate-90">
                    <path
                      d="m190.707 180.101-47.078-47.077c11.702-14.072 18.752-32.142 18.752-51.831C162.381 36.423 125.959 0 81.191 0 36.422 0 0 36.423 0 81.193c0 44.767 36.422 81.187 81.191 81.187 19.688 0 37.759-7.049 51.831-18.751l47.079 47.078a7.474 7.474 0 0 0 5.303 2.197 7.498 7.498 0 0 0 5.303-12.803zM15 81.193C15 44.694 44.693 15 81.191 15c36.497 0 66.189 29.694 66.189 66.193 0 36.496-29.692 66.187-66.189 66.187C44.693 147.38 15 117.689 15 81.193z">
                    </path>
                  </svg>
                  <input type="email" placeholder="Search Something..." className="w-full outline-none bg-transparent text-gray-600 text-sm" />
                </div>
                <div className="absolute top-full left-0 p-4 rounded-lg shadow-lg z-[9999] w-[200%] bg-white hidden">
                  <div className="grid grid-cols-2 gap-x-4">
                    <div>
                      <div>
                        <h3 className="uppercase text-base font-semibold mb-2">Video</h3>
                        <div>
                          <div className="flex items-center cursor-pointer hover:bg-gray-100 rounded-md py-2 px-3 mb-2">
                            <div>
                              <img src="http://localhost:8080/_next/image?url=https%3A%2F%2Floremflickr.com%2F320%2F240%3Flock%3D311219101&w=384&q=75" className="w-[64px] h-[40px] object-cover rounded-md" alt="" />
                            </div>
                            <div className="ml-3">
                              <p className="text-sm font-semibold">Title Title Title Title Title Title Title Title Title</p>
                            </div>
                          </div>
                          <div className="flex items-center cursor-pointer hover:bg-gray-100 rounded-md py-2 px-3 mb-2">
                            <div>
                              <img src="http://localhost:8080/_next/image?url=https%3A%2F%2Floremflickr.com%2F320%2F240%3Flock%3D311219101&w=384&q=75" className="w-[64px] h-[40px] object-cover rounded-md" alt="" />
                            </div>
                            <div className="ml-3">
                              <p className="text-sm font-semibold">Title Title Title Title Title Title Title Title Title</p>
                            </div>
                          </div>
                          <div className="flex items-center cursor-pointer hover:bg-gray-100 rounded-md py-2 px-3 mb-2">
                            <div>
                              <img src="http://localhost:8080/_next/image?url=https%3A%2F%2Floremflickr.com%2F320%2F240%3Flock%3D311219101&w=384&q=75" className="w-[64px] h-[40px] object-cover rounded-md" alt="" />
                            </div>
                            <div className="ml-3">
                              <p className="text-sm font-semibold">Title Title Title Title Title Title Title Title Title</p>
                            </div>
                          </div>
                        </div>
                      </div>
                      <div>
                        <h3 className="uppercase text-base font-semibold mb-2">Video</h3>
                        <div>
                          <div className="flex items-center cursor-pointer hover:bg-gray-100 rounded-md py-2 px-3 mb-2">
                            <div>
                              <img src="http://localhost:8080/_next/image?url=https%3A%2F%2Floremflickr.com%2F320%2F240%3Flock%3D311219101&w=384&q=75" className="w-[64px] h-[40px] object-cover rounded-md" alt="" />
                            </div>
                            <div className="ml-3">
                              <p className="text-sm font-semibold">Title Title Title Title Title Title Title Title Title</p>
                            </div>
                          </div>
                          <div className="flex items-center cursor-pointer hover:bg-gray-100 rounded-md py-2 px-3 mb-2">
                            <div>
                              <img src="http://localhost:8080/_next/image?url=https%3A%2F%2Floremflickr.com%2F320%2F240%3Flock%3D311219101&w=384&q=75" className="w-[64px] h-[40px] object-cover rounded-md" alt="" />
                            </div>
                            <div className="ml-3">
                              <p className="text-sm font-semibold">Title Title Title Title Title Title Title Title Title</p>
                            </div>
                          </div>
                          <div className="flex items-center cursor-pointer hover:bg-gray-100 rounded-md py-2 px-3 mb-2">
                            <div>
                              <img src="http://localhost:8080/_next/image?url=https%3A%2F%2Floremflickr.com%2F320%2F240%3Flock%3D311219101&w=384&q=75" className="w-[64px] h-[40px] object-cover rounded-md" alt="" />
                            </div>
                            <div className="ml-3">
                              <p className="text-sm font-semibold">Title Title Title Title Title Title Title Title Title</p>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div>
                      <div>
                        <h3 className="uppercase text-base font-semibold mb-2">Người Dùng</h3>
                        <div>
                          <div className="flex items-center cursor-pointer hover:bg-gray-100 rounded-md py-2 px-3 mb-2">
                            <div>
                              <img src="http://localhost:8080/_next/image?url=https%3A%2F%2Floremflickr.com%2F320%2F240%3Flock%3D311219101&w=384&q=75" className="w-[64px] h-[40px] object-cover rounded-md" alt="" />
                            </div>
                            <div className="ml-3">
                              <p className="text-sm font-semibold">Title Title Title Title Title Title Title Title Title</p>
                            </div>
                          </div>
                          <div className="flex items-center cursor-pointer hover:bg-gray-100 rounded-md py-2 px-3 mb-2">
                            <div>
                              <img src="http://localhost:8080/_next/image?url=https%3A%2F%2Floremflickr.com%2F320%2F240%3Flock%3D311219101&w=384&q=75" className="w-[64px] h-[40px] object-cover rounded-md" alt="" />
                            </div>
                            <div className="ml-3">
                              <p className="text-sm font-semibold">Title Title Title Title Title Title Title Title Title</p>
                            </div>
                          </div>
                          <div className="flex items-center cursor-pointer hover:bg-gray-100 rounded-md py-2 px-3 mb-2">
                            <div>
                              <img src="http://localhost:8080/_next/image?url=https%3A%2F%2Floremflickr.com%2F320%2F240%3Flock%3D311219101&w=384&q=75" className="w-[64px] h-[40px] object-cover rounded-md" alt="" />
                            </div>
                            <div className="ml-3">
                              <p className="text-sm font-semibold">Title Title Title Title Title Title Title Title Title</p>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
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
        </div>
      </nav>
    </header>
  );
}
