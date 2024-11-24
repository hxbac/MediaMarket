export default function CardHorizontal() {
  return (
    <div className="rounded-lg overflow-hidden shadow-lg px-6 py-4 flex bg-[#f8f7fa]">
      <div className="flex-1 flex items-stretch">
        <div className="rounded-full bg-purple-600 w-14 h-14 mr-4">

        </div>
        <div className="text-sm">
          <div className="mb-1">
            By <b>Gaurav Taneja</b> 20 followers
          </div>
          <div>105 events hosted</div>
        </div>
      </div>
      <div className="flex items-center">
        <div className="bg-purple-600 text-white font-bold text-sm px-8 p-2 select-none cursor-pointer rounded-md hover:opacity-80">Follow</div>
      </div>
    </div>
  );
}
