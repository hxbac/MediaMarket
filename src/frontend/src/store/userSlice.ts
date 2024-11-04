import { UserResponse } from '@/interfaces/user';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface UserState {
  isLoggedIn: boolean;
  user: UserResponse | null;
}

const initialState: UserState = {
  isLoggedIn: false,
  user: null,
};

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    login: (state, action: PayloadAction<UserResponse>) => {
      state.isLoggedIn = true;
      state.user = action.payload;
    },
    logout: (state) => {
      localStorage.removeItem('accessToken');
      state.isLoggedIn = false;
      state.user = null;
    },
  },
});

export const { login, logout } = userSlice.actions;
export default userSlice.reducer;
