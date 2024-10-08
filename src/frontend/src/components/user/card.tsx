export default function Card() {
  return (
    <div className="bg-white rounded-lg overflow-hidden shadow-lg">
      <div className="border-b px-4 pb-6">
        <div className="text-center my-4">
          <img
            className="h-32 w-32 rounded-full border-4 border-white mx-auto my-4"
            src="https://randomuser.me/api/portraits/women/21.jpg"
            alt=""
          />
          <div className="py-2">
            <h3 className="font-bold text-2xl text-gray-800 mb-1">
              Cait Genevieve
            </h3>
            <div className="inline-flex text-gray-700 items-center">
              New York, NY
            </div>
          </div>
        </div>
        <div className="flex gap-2 px-2">
          <button className="flex-1 rounded-full bg-blue-600 text-white antialiased font-bold hover:bg-blue-800 px-4 py-2">
            Follow
          </button>
          <button className="flex-1 rounded-full border-2 border-gray-400 font-semibold text-black px-4 py-2">
            Message
          </button>
        </div>
      </div>
      <div className="px-4 py-4">
        <div className="flex gap-2 items-center text-gray-800 mb-4">
          <svg
            className="h-6 w-6 text-gray-600"
            fill="currentColor"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            width="24"
            height="24"
          >
            <path
              className=""
              d="M12 12a5 5 0 1 1 0-10 5 5 0 0 1 0 10zm0-2a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm9 11a1 1 0 0 1-2 0v-2a3 3 0 0 0-3-3H8a3 3 0 0 0-3 3v2a1 1 0 0 1-2 0v-2a5 5 0 0 1 5-5h8a5 5 0 0 1 5 5v2z"
            />
          </svg>
          <span>
            <strong className="text-black">12</strong> Followers
            you know
          </span>
        </div>
        <div className="flex">
          <div className="flex justify-end mr-2">
            <img
              className="border-2 border-white rounded-full h-10 w-10 -mr-2"
              src="https://randomuser.me/api/portraits/men/32.jpg"
              alt=""
            />
            <img
              className="border-2 border-white rounded-full h-10 w-10 -mr-2"
              src="https://randomuser.me/api/portraits/women/31.jpg"
              alt=""
            />
            <img
              className="border-2 border-white rounded-full h-10 w-10 -mr-2"
              src="https://randomuser.me/api/portraits/men/33.jpg"
              alt=""
            />
            <img
              className="border-2 border-white rounded-full h-10 w-10 -mr-2"
              src="https://randomuser.me/api/portraits/women/32.jpg"
              alt=""
            />
            <img
              className="border-2 border-white rounded-full h-10 w-10 -mr-2"
              src="https://randomuser.me/api/portraits/men/44.jpg"
              alt=""
            />
            <img
              className="border-2 border-white rounded-full h-10 w-10 -mr-2"
              src="https://randomuser.me/api/portraits/women/42.jpg"
              alt=""
            />
            <span className="flex items-center justify-center bg-white text-sm text-gray-800 font-semibold border-2 border-gray-200 rounded-full h-10 w-10">
              +999
            </span>
          </div>
        </div>
      </div>
    </div>
  );
}
