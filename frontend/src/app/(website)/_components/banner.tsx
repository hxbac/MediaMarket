export default function Banner() {
  return (
    <section className="bg-white">
      <div className="grid max-w-screen-xl px-4 pt-20 pb-8 mx-auto lg:gap-8 xl:gap-0 lg:py-16 lg:grid-cols-12 lg:pt-28">
        <div className="mr-auto place-self-center lg:col-span-7">
          <h1 className="max-w-2xl mb-4 text-4xl font-extrabold leading-none tracking-tight md:text-5xl xl:text-6xl">
            Building digital <br />
            products &amp; brands.
          </h1>
          <p className="max-w-2xl mb-6 font-light text-gray-500 lg:mb-8 md:text-lg lg:text-xl">
            This free and open-source landing page template was built using the
            utility classes from{" "}
            <a href="https://tailwindcss.com" className="hover:underline">
              Tailwind CSS
            </a>{" "}
            and based on the components from the{" "}
            <a
              href="https://flowbite.com/docs/getting-started/introduction/"
              className="hover:underline"
            >
              Flowbite Library
            </a>{" "}
            and the{" "}
            <a href="https://flowbite.com/blocks/" className="hover:underline">
              Blocks System
            </a>
            .
          </p>
          <div className="space-y-4 sm:flex sm:space-y-0 sm:space-x-4">
            <a
              href="https://github.com/themesberg/landwind"
              className="inline-flex items-center justify-center w-full px-5 py-3 text-sm font-medium text-center text-gray-900 border border-gray-200 rounded-lg sm:w-auto hover:bg-gray-100 focus:ring-4 focus:ring-gray-100"
            >
              View on GitHub
            </a>
            <a
              href="https://www.figma.com/community/file/1125744163617429490"
              className="inline-flex items-center justify-center w-full px-5 py-3 mb-2 mr-2 text-sm font-medium text-gray-900 bg-white border border-gray-200 rounded-lg sm:w-auto focus:outline-none hover:bg-gray-100 hover:text-blue-700 focus:z-10 focus:ring-4 focus:ring-gray-200"
            >
              Get Figma file
            </a>
          </div>
        </div>
        <div className="hidden lg:mt-0 lg:col-span-5 lg:flex">
          <img src="https://static.sellfy.com/assets/images/public/1720620680.hero.png" alt="hero image" />
        </div>
      </div>
    </section>
  );
}
