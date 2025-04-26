const observer = new IntersectionObserver(
  (entries) => {
    entries.forEach((entry) => {
      const el = entry.target;
      if (entry.isIntersecting) {
        el.classList.add("opacity-100", "translate-y-0");
        el.classList.remove("opacity-0", "translate-y-5");
      } else {
        el.classList.remove("opacity-100", "translate-y-0");
        el.classList.add("opacity-0", "translate-y-5");
      }
    });
  },
  {
    threshold: 0.2,
  }
);

document
  .querySelectorAll(".fade-on-scroll")
  .forEach((el) => observer.observe(el));

function hoverArrow(isHover) {
  const img = document.getElementById("arrowIcon");
  img.src = isHover ? "../images/Arrow 2.svg" : "../images/Arrow 1.svg";
}

function hoverArrow2(isHover) {
  const img = document.getElementById("arrow2Icon");
  img.src = isHover ? "../images/Arrow 3.svg" : "../images/Arrow 2.svg";
}

function redirectFunction() {
  document.getElementById("main").scrollIntoView({
    behavior: "smooth",
  });
}

const paragraphText = `Welcome into the world of warehouse management.`;
const typewriterEl = document.getElementById("typewriter");

let index = 0;
let isDeleting = false;

function typeWriterEffect() {
  if (!isDeleting && index <= paragraphText.length) {
    typewriterEl.innerHTML = paragraphText.substring(0, index);
    index++;
    setTimeout(typeWriterEffect, 50);
  } else if (isDeleting && index >= 0) {
    typewriterEl.innerHTML = paragraphText.substring(0, index);
    index--;
    setTimeout(typeWriterEffect, 45);
  } else {
    isDeleting = !isDeleting;
    setTimeout(typeWriterEffect, 2000);
  }
}

typeWriterEffect();

function flipCardNext() {
  document.getElementById("flip-card").classList.add("rotate-y-180");
}

function flipCardBack() {
  document.getElementById("flip-card").classList.remove("rotate-y-180");
}
