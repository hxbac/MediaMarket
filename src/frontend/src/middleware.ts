// src/middleware.ts

import { RequiredLoginMiddleware } from './middlewares/RequiredLoginMiddleware';
import type { NextRequest } from 'next/server';

export function middleware(request: NextRequest) {
  const response = RequiredLoginMiddleware(request);

  return response;
}

export const config = {
  matcher: ['/'],
};
