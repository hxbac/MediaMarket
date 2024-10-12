export default function Category({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <div
      className="py-1 px-4 font-bold whitespace-nowrap transition-all border border-gray-700 rounded-[100px] hover:bg-purple-600 hover:border-purple-600 hover:text-white cursor-pointer"
    >
     {children}
    </div>
  );
}
