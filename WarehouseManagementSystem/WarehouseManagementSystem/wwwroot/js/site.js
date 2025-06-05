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

function toggleDropdown(prefix) {
    const dropdown = document.getElementById(`dropdown${prefix}Content`);
    const arrow = document.getElementById(`${prefix}Arrow`);
    dropdown.classList.toggle("hidden");
    arrow.classList.toggle("custom-rotate-neg90");
}

function selectDropdownItem(value, text, prefix) {
    const display = document.getElementById(`${prefix}Display`);
    const hiddenId = document.getElementById(`${prefix}Id`);
    const hiddenName = document.getElementById(`${prefix}Name`);

    display.textContent = text;
    hiddenId.value = value;
    hiddenName.value = text;

    $(`#${prefix}Id`).valid();
    $(`#${prefix}Name`).valid();

    toggleDropdown(prefix);
}

document.addEventListener("click", function (e) {
    const dropdowns = ["Category", "Product", "Zone"];
    dropdowns.forEach(prefix => {
        const dropdown = document.getElementById(`dropdown${prefix}Content`);
        const toggle = document.querySelector(`.toggle-${prefix.toLowerCase()}dropdown`);
        const arrow = document.getElementById(`${prefix}Arrow`);
        if (toggle && !toggle.contains(e.target)) {
            dropdown.classList.add("hidden");
            arrow.classList.remove("custom-rotate-neg90");
        }
    });
});