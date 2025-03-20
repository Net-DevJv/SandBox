function initImageImport(buttonId, fileId, textId) {
    const btn = document.getElementById(buttonId);
    const fileInput = document.getElementById(fileId);
    const textBox = document.getElementById(textId);

    if (!btn || !fileInput || !textBox) {
        console.error("Algum elemento não foi encontrado pelo ID.");
        return;
    }

    btn.addEventListener('click', function () {
        fileInput.click();
    });

    fileInput.addEventListener('change', function (e) {
        if (e.target.files && e.target.files.length > 0) {
            const fileName = e.target.files[0].name;
            const path = "wwwroot/img/products/" + fileName;
            textBox.value = path;
        }
    });
}

document.addEventListener('DOMContentLoaded', function () {
    initImageImport('btnImportar', 'fileInput', 'ImageUrl');
});

function sortTableByColumn(table, colIndex, asc) {
    const tbody = table.querySelector("tbody");
    const rows = Array.from(tbody.querySelectorAll("tr"));

    rows.sort((a, b) => {
        const cellA = a.querySelectorAll("td")[colIndex]?.innerText.trim() || "";
        const cellB = b.querySelectorAll("td")[colIndex]?.innerText.trim() || "";

        const numA = parseFloat(cellA.replace(",", ".").replace("R$", ""));
        const numB = parseFloat(cellB.replace(",", ".").replace("R$", ""));

        if (!isNaN(numA) && !isNaN(numB)) {
            return asc ? (numA - numB) : (numB - numA);
        } else {
            return asc ? cellA.localeCompare(cellB) : cellB.localeCompare(cellA);
        }
    });

    rows.forEach(row => tbody.appendChild(row));
}
function updateArrows(table, clickedTh, ascending) {
    table.querySelectorAll("th.sortable .arrow-icon").forEach(icon => {
        icon.classList.remove("bi-chevron-up", "bi-chevron-down");
        icon.style.display = "none";
    });

    const arrowIcon = clickedTh.querySelector(".arrow-icon");
    if (!arrowIcon) return;

    if (ascending) {
        arrowIcon.classList.add("bi-chevron-up");
    } else {
        arrowIcon.classList.add("bi-chevron-down");
    }
    arrowIcon.style.display = "inline-block";
}

document.addEventListener("DOMContentLoaded", () => {
    const table = document.getElementById("myTable");
    if (!table) return;

    let currentCol = null;
    let ascending = true;

    table.querySelectorAll("th.sortable").forEach(th => {
        th.addEventListener("click", () => {
            const colIndex = parseInt(th.getAttribute("data-col"));

            if (colIndex === currentCol) {
                ascending = !ascending;
            } else {
                ascending = true;
                currentCol = colIndex;
            }

            sortTableByColumn(table, colIndex, ascending);

            updateArrows(table, th, ascending);
        });
    });
});