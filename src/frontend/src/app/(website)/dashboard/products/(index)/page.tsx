'use client';

import Actions from './_components/actions';
import { SearchProductContextProvider } from '../_context/SearchProductContext';
import { SearchProductParams } from '@/interfaces/products';
import ProductTable from './_components/productTable';
import { useState } from 'react';

export default function Page() {
  const [searchParams, setSearchParams] = useState<SearchProductParams>({
    name: '',
    productType: null
  });

  return (
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl px-4 py-8 mx-auto lg:gap-16 xl:gap-24 lg:py-24 lg:px-6">
        <h1 className="text-center uppercase text-xl font-bold mb-8">Quản lý sản phẩm</h1>
        <SearchProductContextProvider initial={{
          value: searchParams,
          setValue: setSearchParams
         }}>
          <div>
            <Actions />
          </div>
          <ProductTable />
        </SearchProductContextProvider>
      </div>
    </section>
  );
}
