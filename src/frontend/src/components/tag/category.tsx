export default function Category({
  children,
  onclick,
  selected
}: Readonly<{
  children: string;
  onclick: () => void;
  selected: boolean;
  key: string;
}>) {
  return (
    <div onClick={onclick}
      className={`py-1 px-4 font-bold whitespace-nowrap transition-all border border-gray-700 rounded-[100px] hover:bg-purple-600 hover:border-purple-600 hover:text-white cursor-pointer ${selected ? 'bg-purple-600 text-white' : ''}`}
    >
     {children}
    </div>
  );
}
