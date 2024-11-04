import { ProductInfo } from '@/interfaces/products';
import React, { createContext, useContext, ReactNode } from 'react';

interface ContextType {
  value: ProductInfo;
  setValue: React.Dispatch<React.SetStateAction<ProductInfo>>;
}

const ProductContext = createContext<ContextType | undefined>(undefined);

export const ProductContextProvider: React.FC<{ children: ReactNode, initial: ContextType }> = ({ children, initial }) => {
  return (
    <ProductContext.Provider value={initial}>
      {children}
    </ProductContext.Provider>
  );
};

export const useProductContext = () => {
  const context = useContext(ProductContext);
  if (!context) {
    throw new Error('useProductContext must be used within a ProductContextProvider');
  }
  return context;
};
