function toggleRoleDropdown() {
  const dropdown = document.getElementById("dropdownRoleContent");
  const arrow = document.getElementById("dropdownArrow");
  dropdown.classList.toggle("hidden");
  arrow.classList.toggle("custom-rotate-neg90");
}

function selectRole(value, text) {
  const display = document.getElementById("selectedRole");
  const hiddenInput = document.getElementById("SelectedRole");

  display.textContent = text;
  hiddenInput.value = value;

  $(hiddenInput).valid();

  toggleRoleDropdown();
}

document.addEventListener("click", function (e) {
  const dropdown = document.getElementById("dropdownRoleContent");
  const toggle = document.querySelector(".toggle-roledropdown");
  if (!toggle.contains(e.target)) {
    dropdown.classList.add("hidden");
    document
      .getElementById("dropdownArrow")
      .classList.remove("custom-rotate-neg90");
  }
});

const btn = document.getElementById("dropdownBtn");
const menu = document.getElementById("dropdownMenu");

btn.addEventListener("click", () => {
  menu.classList.toggle("hidden");
  document
    .getElementById("inventoryDropdownArrow")
    .classList.toggle("custom-rotate-neg90");
});

document.addEventListener("click", (e) => {
  if (!btn.contains(e.target) && !menu.contains(e.target)) {
    menu.classList.add("hidden");
    document
      .getElementById("inventoryDropdownArrow")
      .classList.remove("custom-rotate-neg90");
  }
});
