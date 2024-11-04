import { NextResponse } from 'next/server';
import type { NextRequest } from 'next/server';

export function RequiredLoginMiddleware(request: NextRequest) {
  // const token = localStorage.getItem('accessToken');

  // console.log({ token });

  // if (!token && request.nextUrl.pathname.startsWith('/protected')) {
  //   return NextResponse.redirect(new URL('/login', request.url));
  // }

  return NextResponse.next();
}
