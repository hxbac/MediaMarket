import Link from "next/link";

export default function Page() {
  return (
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl px-4 py-8 mx-auto lg:py-24 lg:px-6 grid grid-cols-1 gap-4 md:grid-cols-2 md:gap-6 xl:grid-cols-3 2xl:gap-8">
        <div className="flex justify-between rounded-sm border border-stroke bg-white px-8 py-6 shadow-default">
          <div>
            <div className="flex h-10 w-10 items-center justify-center rounded-full bg-[#eff2f7]">
              <svg
                fill="#3C50E0"
                width="20px"
                height="20px"
                viewBox="0 0 24.00 24.00"
                xmlns="http://www.w3.org/2000/svg"
                stroke="#3C50E0"
                strokeWidth="0.00024000000000000003"
              >
                <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                <g
                  id="SVGRepo_tracerCarrier"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                ></g>
                <g id="SVGRepo_iconCarrier">
                  <path d="M4,23a1,1,0,0,1-1-1V19a1,1,0,0,1,2,0v3A1,1,0,0,1,4,23Zm9-1V15a1,1,0,0,0-2,0v7a1,1,0,0,0,2,0Zm7-11a1,1,0,0,0-1,1V22a1,1,0,0,0,2,0V12A1,1,0,0,0,20,11Zm.382-9.923A.991.991,0,0,0,20,1H16a1,1,0,0,0,0,2h1.586L12,8.586,8.707,5.293a1,1,0,0,0-1.414,0l-4,4a1,1,0,0,0,1.414,1.414L8,7.414l3.293,3.293a1,1,0,0,0,1.414,0L19,4.414V6a1,1,0,0,0,2,0V2a1,1,0,0,0-.618-.923Z"></path>
                </g>
              </svg>
            </div>

            <div className="mt-4 flex text-sm items-end justify-between relative">
              <div>
                <h4 className="text-title-md font-bold text-black">0đ</h4>
                <span className="text-sm font-medium">Total Revenue</span>
              </div>
            </div>
          </div>

          <div className="flex flex-col justify-end">
            <div className="flex justify-end">
              <h4 className="text-sm font-semibold text-green-600 mr-2">+0đ</h4>
              <span className="text-sm font-medium">Today</span>
            </div>
            <div className="flex justify-end">
              <h4 className="text-sm font-semibold text-green-600 mr-2">+0đ</h4>
              <span className="text-sm font-medium">Last Week</span>
            </div>
            <div className="flex justify-end">
              <h4 className="text-sm font-semibold text-green-600 mr-2">+0đ</h4>
              <span className="text-sm font-medium">Last Month</span>
            </div>
          </div>
        </div>

        <div className="flex justify-between rounded-sm border border-stroke bg-white px-8 py-6 shadow-default">
          <div>
            <div className="flex h-10 w-10 items-center justify-center rounded-full bg-[#eff2f7]">
              <svg
                width="20px"
                height="20px"
                viewBox="0 0 1024 1024"
                className="icon"
                version="1.1"
                xmlns="http://www.w3.org/2000/svg"
                fill="#3C50E0"
              >
                <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                <g
                  id="SVGRepo_tracerCarrier"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                ></g>
                <g id="SVGRepo_iconCarrier">
                  <path
                    d="M670.5 471.7c-7.1-3.1-14.2-5.9-21.4-8.5 49.8-40.3 81.6-101.8 81.6-170.6 0-121-98.4-219.4-219.4-219.4s-219.4 98.4-219.4 219.4c0 68.9 31.9 130.5 81.7 170.7C219.4 519.6 109 667.8 109 841.3h73.1c0-181.5 147.7-329.1 329.1-329.1 45.3 0 89.1 9 130.2 26.7l29.1-67.2zM511.3 146.3c80.7 0 146.3 65.6 146.3 146.3S592 438.9 511.3 438.9 365 373.2 365 292.6s65.6-146.3 146.3-146.3zM612.5 636.5c0 10.2 5.6 19.5 14.6 24.2l128 67.6c4 2.1 8.4 3.2 12.8 3.2s8.8-1.1 12.8-3.2l128-67.6c9-4.8 14.6-14.1 14.6-24.2s-5.6-19.5-14.6-24.2l-128-67.7c-8-4.2-17.6-4.2-25.6 0l-128 67.7c-9 4.7-14.6 14-14.6 24.2z m155.4-36.6l69.3 36.6-69.3 36.6-69.3-36.6 69.3-36.6z"
                    fill="#3C50E0"
                  ></path>
                  <path
                    d="M767.9 763.4l-147-77.7-25.6 48.5 172.6 91.2 171.9-90.8-25.6-48.5z"
                    fill="#3C50E0"
                  ></path>
                  <path
                    d="M767.9 851.4l-147-77.6-25.6 48.4 172.6 91.3 171.3-90.6-25.6-48.5z"
                    fill="#3C50E0"
                  ></path>
                </g>
              </svg>
            </div>

            <div className="mt-4 flex items-end justify-between">
              <div>
                <h4 className="text-title-md font-bold text-black">2</h4>
                <span className="text-sm font-medium">Returning Customers</span>
              </div>
            </div>
          </div>

          <div className="flex flex-col justify-end">
            <div className="flex justify-end">
              <h4 className="text-sm font-semibold text-green-600 mr-2">+0</h4>
              <span className="text-sm font-medium">Today</span>
            </div>
            <div className="flex justify-end">
              <h4 className="text-sm font-semibold text-green-600 mr-2">+0</h4>
              <span className="text-sm font-medium">Last Week</span>
            </div>
            <div className="flex justify-end">
              <h4 className="text-sm font-semibold text-green-600 mr-2">+2</h4>
              <span className="text-sm font-medium">Last Month</span>
            </div>
          </div>
        </div>

        <div className="flex justify-between rounded-sm border border-stroke bg-white px-8 py-6 shadow-default">
          <div>
            <div className="flex h-10 w-10 items-center justify-center rounded-full bg-[#eff2f7]">
              <svg
                width="20px"
                height="20px"
                viewBox="0 0 1024 1024"
                className="icon"
                version="1.1"
                xmlns="http://www.w3.org/2000/svg"
                fill="#3C50E0"
              >
                <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                <g
                  id="SVGRepo_tracerCarrier"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                ></g>
                <g id="SVGRepo_iconCarrier">
                  <path
                    d="M670.5 471.7c-7.1-3.1-14.2-5.9-21.4-8.5 49.8-40.3 81.6-101.8 81.6-170.6 0-121-98.4-219.4-219.4-219.4s-219.4 98.4-219.4 219.4c0 68.9 31.9 130.5 81.7 170.7C219.4 519.6 109 667.8 109 841.3h73.1c0-181.5 147.7-329.1 329.1-329.1 45.3 0 89.1 9 130.2 26.7l29.1-67.2zM511.3 146.3c80.7 0 146.3 65.6 146.3 146.3S592 438.9 511.3 438.9 365 373.2 365 292.6s65.6-146.3 146.3-146.3zM612.5 636.5c0 10.2 5.6 19.5 14.6 24.2l128 67.6c4 2.1 8.4 3.2 12.8 3.2s8.8-1.1 12.8-3.2l128-67.6c9-4.8 14.6-14.1 14.6-24.2s-5.6-19.5-14.6-24.2l-128-67.7c-8-4.2-17.6-4.2-25.6 0l-128 67.7c-9 4.7-14.6 14-14.6 24.2z m155.4-36.6l69.3 36.6-69.3 36.6-69.3-36.6 69.3-36.6z"
                    fill="#3C50E0"
                  ></path>
                  <path
                    d="M767.9 763.4l-147-77.7-25.6 48.5 172.6 91.2 171.9-90.8-25.6-48.5z"
                    fill="#3C50E0"
                  ></path>
                  <path
                    d="M767.9 851.4l-147-77.6-25.6 48.4 172.6 91.3 171.3-90.6-25.6-48.5z"
                    fill="#3C50E0"
                  ></path>
                </g>
              </svg>
            </div>

            <div className="mt-4 flex items-end justify-between">
              <div>
                <h4 className="text-title-md font-bold text-black">2</h4>
                <span className="text-sm font-medium">Returning Customers</span>
              </div>
            </div>
          </div>

          <div className="flex flex-col justify-end">
            <div className="flex justify-end">
              <Link href={'/dashboard/withdrawal'}>Rút tiền</Link>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}
