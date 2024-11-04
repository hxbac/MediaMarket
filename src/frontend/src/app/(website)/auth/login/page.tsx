'use client';

import { useState } from "react";

import authService from '@/services/authService.js';
import { toast } from "react-toastify";
import { useAppDispatch } from "@/store/hooks";
import { login } from '@/store/userSlice';
import { UserResponse } from "@/interfaces/user";
import { useRouter } from "next/navigation";

interface AuthRequest {
  email: string;
  password: string;
}

interface LoginResponse {
  token: string;
  user: UserResponse
}

export default function Page() {
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const dispatch = useAppDispatch();
  const router = useRouter();

  const handleLogin = async () => {
    const data : AuthRequest = {
      email,
      password
    }
    try {
      const result = await authService.login(data);
      if (result.succeeded) {
        const data : LoginResponse = result.data;
        localStorage.setItem('accessToken', data.token);
        dispatch(login(data.user));
        toast.success('Đăng nhập thành công!');
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
              <p className="text-[32px] font-bold text-zinc-950">Sign In</p>
              <p className="mb-2.5 mt-2.5 font-normal text-zinc-950">
                Enter your email and password to sign in!
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
                      <label className="text-zinc-950 mt-2">Password</label>
                      <input
                        id="password"
                        placeholder="Password"
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        className="mr-2.5 mb-2 h-full min-h-[44px] w-full rounded-lg border border-zinc-200 bg-white px-4 py-3 text-sm font-medium text-zinc-950 placeholder:text-zinc-400 focus:outline-0"
                        name="password"
                      />
                    </div>
                    <button
                      onClick={handleLogin}
                      className="whitespace-nowrap ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 bg-gray-900 text-primary-foreground hover:opacity-80 mt-2 flex h-[unset] w-full items-center justify-center rounded-lg px-4 py-4 text-sm font-medium text-white"
                      type="submit"
                    >
                      Sign in
                    </button>
                  </div>
                </div>
                <p>
                  <a
                    href="/dashboard/signin/forgot_password"
                    className="font-medium text-zinc-950 text-sm"
                  >
                    Forgot your password?
                  </a>
                </p>
                <p>
                  <a
                    href="/dashboard/signin/email_signin"
                    className="font-medium text-zinc-950 text-sm"
                  >
                    Sign in via magic link
                  </a>
                </p>
                <p>
                  <a
                    href="/dashboard/signin/signup"
                    className="font-medium text-zinc-950 text-sm"
                  >
                    Dont have an account? Sign up
                  </a>
                </p>
              </div>
            </div>
          </div>

          <p className="font-normal text-zinc-950 mt-20 mx-auto w-max">
            Auth Form from{" "}
            <a
              href="https://horizon-ui.com/shadcn-ui?ref=twcomponents"
              target="_blank"
              className="text-brand-500 font-bold"
            >
              Horizon AI Boilerplate
            </a>
          </p>
        </div>
      </section>
    </div>
  );
}
