import { configureStore } from '@reduxjs/toolkit';
import userReducer from './userSlice';

export const makeStore = () => {
  return configureStore({
    reducer: {
      user: userReducer,
    },
  })
};

export type RootState = ReturnType<AppStore['getState']>;
export type AppDispatch = AppStore['dispatch'];
export type AppStore = ReturnType<typeof makeStore>;
