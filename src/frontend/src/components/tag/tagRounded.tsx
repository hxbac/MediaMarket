export default function TagRounded({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <div className="rounded-[48px] text-[#39364f] h-9 px-4 bg-[#f8f7fa] hover:opacity-80 select-none cursor-pointer flex items-center justify-center">
      {children}
    </div>
  );
}
