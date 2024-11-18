import type { Config } from "tailwindcss";

const config: Config = {
  content: [
    "./src/pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/components/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/app/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    extend: {
      colors: {
        background: "var(--background)",
        foreground: "var(--foreground)",
      },
      fontSize: {
        'xtiny': '.8125rem',
        '4xl': '1.5625rem',
        '3xl': '1.5rem',
        '2xl': '1.375rem',
        'tiny': '.875rem',
        'base': '1rem',
        '6xl': '1.75rem',
        '5xl': '1.625rem',
        '7xl': '2.5rem',
        'lg': '1.125rem',
      }
    },
  },
  plugins: [],
};
export default config;
