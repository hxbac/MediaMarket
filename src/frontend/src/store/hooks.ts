import { useDispatch, useSelector } from 'react-redux';
import type { RootState, AppDispatch, AppStore } from './store';
import { useStore } from 'react-redux';

export const useAppDispatch = useDispatch.withTypes<AppDispatch>()
export const useAppSelector = useSelector.withTypes<RootState>()
export const useAppStore = useStore.withTypes<AppStore>()
