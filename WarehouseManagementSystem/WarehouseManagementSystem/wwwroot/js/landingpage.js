document.querySelectorAll(".dropdown-group").forEach((group) => {
  const toggle = group.querySelector(".dropdown-toggle");
  const content = group.querySelector(".dropdown-content");
  const arrow = group.querySelector(".div-arrow-icon");

  let open = false;

  toggle.addEventListener("click", () => {
    open = !open;
    content.classList.toggle("hidden", !open);
    content.classList.toggle("scale-100", open);
    content.classList.toggle("scale-95", !open);
    toggle.classList.toggle("scale-105", open);
    toggle.classList.toggle("scale-100", !open);
    arrow.classList.toggle("rotate-90", open);
    arrow.classList.toggle("rotate-0", !open);
  });

  document.addEventListener("click", (e) => {
    if (!group.contains(e.target) && open) {
      open = false;
      content.classList.add("hidden");
      content.classList.remove("scale-100");
      content.classList.add("scale-95");
      toggle.classList.remove("scale-105");
      toggle.classList.add("scale-100");
      arrow.classList.remove("rotate-90");
      arrow.classList.add("rotate-0");
    }
  });
});
