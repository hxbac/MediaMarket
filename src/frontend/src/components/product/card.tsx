import Image from "next/image";

export default function Card() {
  return (
    <div className="relative max-w-sm bg-white border border-gray-200 rounded-lg shadow">
      <div className="card-top-overlay text-white font-bold pt-3 pl-3 text-lg">abc</div>
      <div>
        <Image
          src="https://static.skillshare.com/uploads/video/thumbnails/dc94003f94071d4bbf5e7f68775803db/448-252"
          alt=""
          width={150}
          height={268}
          className="w-full h-[168px] rounded-t-lg object-cover object-center"
        />
      </div>
      <div className="p-2">
        <div className="flex items-center justify-between mt-2 mb-1">
          <span className="text-xs font-semibold">37,210 students</span>
          <span className="text-xs font-semibold">51m</span>
        </div>
        <h5 className="mb-2 text-base font-bold tracking-tight text-gray-900">
          Noteworthy technology acquisitions 2021
        </h5>
        <p className="mb-3 font-sm text-gray-700">
          Here are the biggest enterprise technology acquisitions of 2021 so
        </p>
      </div>
    </div>
  );
}
