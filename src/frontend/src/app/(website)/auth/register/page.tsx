'use client';

import { useState } from "react";

import authService from '@/services/authService.js';
import { toast } from "react-toastify";
import { useAppDispatch } from "@/store/hooks";
import { login } from '@/store/userSlice';
import { UserResponse } from "@/interfaces/user";
import { useRouter } from "next/navigation";

interface RegisterRequest {
  name: string;
  email: string;
  password: string;
  address: string;
  phoneNumber: string;
}

interface LoginResponse {
  token: string;
  user: UserResponse
}

export default function Page() {
  const [name, setName] = useState<string>('');
  const [address, setAddress] = useState<string>('');
  const [phoneNumber, setPhoneNumber] = useState<string>('');
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const dispatch = useAppDispatch();
  const router = useRouter();

  const handleRegister = async () => {
    const data : RegisterRequest = {
      name,
      email,
      password,
      address,
      phoneNumber
    }
    try {
      const result = await authService.register(data);
      if (result.succeeded) {
        const data : LoginResponse = result.data;
        localStorage.setItem('accessToken', data.token);
        dispatch(login(data.user));
        toast.success('Đăng ký thành thành công!');
        router.push('/');
      } else {
        throw new Error(result.message);
      }
    } catch (error: unknown) {
      const errorMessage = error instanceof Error ? error.message : 'An unexpected error occurred';
      toast.error(errorMessage);
    }
  }

  return (
    <div>
      <section className="bg-white">
        <div className="flex flex-col justify-center items-center bg-white h-[100vh]">
          <div className="mx-auto flex w-full flex-col justify-center px-5 pt-0 md:h-[unset] md:max-w-[50%] lg:h-[100vh] min-h-[100vh] lg:max-w-[50%] lg:px-6">
            <div className="my-auto mb-auto mt-8 flex flex-col md:mt-[70px] w-[350px] max-w-[450px] mx-auto md:max-w-[450px] lg:mt-[130px] lg:max-w-[450px]">
              <p className="text-[32px] font-bold text-zinc-950">Đăng ký</p>
              <p className="mb-2.5 mt-2.5 font-normal text-zinc-950">
                Nhập thông tin của bạn!
              </p>
              <div className="relative my-4">
                <div className="relative flex items-center py-1">
                  <div className="grow border-t border-zinc-200"></div>
                  <div className="grow border-t border-zinc-200"></div>
                </div>
              </div>
              <div>
                <div className="mb-4">
                  <div className="grid gap-2">
                    <div className="grid gap-1">
                      <label className="text-zinc-950">Họ tên</label>
                      <input
                        className="mr-2.5 mb-2 h-full min-h-[44px] w-full rounded-lg border border-zinc-200 bg-white px-4 py-3 text-sm font-medium text-zinc-950 placeholder:text-zinc-400 focus:outline-0"
                        id="name"
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                      />
                      <label className="text-zinc-950">Email</label>
                      <input
                        className="mr-2.5 mb-2 h-full min-h-[44px] w-full rounded-lg border border-zinc-200 bg-white px-4 py-3 text-sm font-medium text-zinc-950 placeholder:text-zinc-400 focus:outline-0"
                        id="email"
                        placeholder="name@example.com"
                        type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        name="email"
                      />
                      <label className="text-zinc-950 mt-2">Mật khẩu</label>
                      <input
                        id="password"
                        placeholder="Password"
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        className="mr-2.5 mb-2 h-full min-h-[44px] w-full rounded-lg border border-zinc-200 bg-white px-4 py-3 text-sm font-medium text-zinc-950 placeholder:text-zinc-400 focus:outline-0"
                        name="password"
                      />
                      <label className="text-zinc-950">Địa chỉ</label>
                      <input
                        className="mr-2.5 mb-2 h-full min-h-[44px] w-full rounded-lg border border-zinc-200 bg-white px-4 py-3 text-sm font-medium text-zinc-950 placeholder:text-zinc-400 focus:outline-0"
                        id="address"
                        type="text"
                        value={address}
                        onChange={(e) => setAddress(e.target.value)}
                      />
                      <label className="text-zinc-950">Số điện thoại</label>
                      <input
                        className="mr-2.5 mb-2 h-full min-h-[44px] w-full rounded-lg border border-zinc-200 bg-white px-4 py-3 text-sm font-medium text-zinc-950 placeholder:text-zinc-400 focus:outline-0"
                        id="PhoneNumber"
                        type="text"
                        value={phoneNumber}
                        onChange={(e) => setPhoneNumber(e.target.value)}
                      />
                    </div>
                    <button
                      onClick={handleRegister}
                      className="whitespace-nowrap ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 bg-gray-900 text-primary-foreground hover:opacity-80 mt-2 flex h-[unset] w-full items-center justify-center rounded-lg px-4 py-4 text-sm font-medium text-white"
                      type="submit"
                    >
                      Đăng ký
                    </button>
                  </div>
                </div>
                <p>
                  <a
                    href="/auth/login"
                    className="font-medium text-zinc-950 text-sm"
                  >
                    Bạn đã có tài khoản? Đăng nhập
                  </a>
                </p>
              </div>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}
