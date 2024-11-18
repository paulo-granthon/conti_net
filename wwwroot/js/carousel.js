document.addEventListener("DOMContentLoaded", function () {
  // Get all carousel components on the page
  const carousels = document.querySelectorAll(".carousel");

  carousels.forEach((carousel) => {
    // Get images from the `data-images` attribute
    const images = JSON.parse(carousel.getAttribute("data-images"));

    // Generate the HTML structure for the carousel
    let carouselHTML = "";
    images.forEach((image) => {
      carouselHTML += `
        <div class="w-full flex-shrink-0">
          <img src="${image}" alt="Image" class="w-full object-cover">
        </div>
      `;
    });
    carousel.innerHTML = `
      <div class="flex transition-transform duration-1000 ease-in-out">
        ${carouselHTML}
      </div>
      <button class="absolute top-1/2 left-0 transform -translate-y-1/2 px-4 py-2 bg-black text-white rounded-full" id="prevBtn">←</button>
      <button class="absolute top-1/2 right-0 transform -translate-y-1/2 px-4 py-2 bg-black text-white rounded-full" id="nextBtn">→</button>
    `;

    const carouselContainer = carousel.querySelector(".flex");
    const prevBtn = carousel.querySelector("#prevBtn");
    const nextBtn = carousel.querySelector("#nextBtn");
    let index = 0;

    // Function to go to the next slide
    const goToNextSlide = () => {
      index = (index + 1) % images.length;
      carouselContainer.style.transform = `translateX(-${index * 100}%)`;
    };

    // Function to go to the previous slide
    const goToPrevSlide = () => {
      index = (index - 1 + images.length) % images.length;
      carouselContainer.style.transform = `translateX(-${index * 100}%)`;
    };

    // Auto-scroll every 5 seconds (5000ms)
    setInterval(goToNextSlide, 5000);

    // Event listeners for next and previous buttons
    nextBtn.addEventListener("click", goToNextSlide);
    prevBtn.addEventListener("click", goToPrevSlide);
  });
});
