import { SearchProductParams } from '@/interfaces/products';
import React, { createContext, useContext, ReactNode } from 'react';

interface ContextType {
  value: SearchProductParams;
  setValue: React.Dispatch<React.SetStateAction<SearchProductParams>>;
}

const SearchProductContext = createContext<ContextType | undefined>(undefined);

export const SearchProductContextProvider: React.FC<{ children: ReactNode, initial: ContextType }> = ({ children, initial }) => {
  return (
    <SearchProductContext.Provider value={initial}>
      {children}
    </SearchProductContext.Provider>
  );
};

export const useSearchProductContext = () => {
  const context = useContext(SearchProductContext);
  if (!context) {
    throw new Error('useProductContext must be used within a ProductContextProvider');
  }
  return context;
};
