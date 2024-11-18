module.exports = {
  content: [
    "./src/Views/**/*.cshtml",
    "./wwwroot/css/**/*.css",
  ],
  media: false,
  theme: {
    extend: {
      colors: {
        gray: {
          200: "#e9e9e9",
          600: "#717171",
          700: "#4c4c4c",
        },
        cyan: {
          100: "#ecf2f3",
          200: "#ddeaed",
          500: "#53c4cd",
          600: "#3c9eb0",
          700: "#1d909f",
          1000: "#03313a",
        },
      },
    },
  },
  plugins: [],
};
