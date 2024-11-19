import Link from "next/link";

export default function Page({
  searchParams,
}: {
  searchParams: { [key: string]: string | string[] | undefined };
}) {
  const query = searchParams.query;

  return (
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl px-4 mx-auto py-8">
        <h1 className="text-6xl font-bold">{query}</h1>
        <div>
          <div className="flex items-end justify-between">
            <h2 className="text-6xl font-bold">Video</h2>
            <Link href={`/search/video?query=${query}`} className="font-bold text-purple-600">Xem tất cả video</Link>
          </div>
          <p>Learn skills, tools, and techniques from industry experts and creative pros.</p>
          <div className="grid grid-cols-4 gap-x-4 gap-y-6">

          </div>
        </div>
      </div>
    </section>
  );
}
