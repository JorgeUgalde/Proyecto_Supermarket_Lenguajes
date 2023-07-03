
function validateUserInformation() {
    var userData = JSON.parse(localStorage.getItem('userData'));
    if (userData == null) {
        window.location.href = "../pages/UserInformation.html";
        // Puedes utilizar window.location.href = 'index.html' para redirigir al usuario
        return;
    }
}


const isActive = 1;



function fetchJsonData() {
    $.ajax({
        url: 'http://proyectoapps-001-site1.atempurl.com/Customer/Home/GetAll',
        type: 'GET',
        success: function (data) {
            generateCards(data);
        }
    });
}

function generateCards(data) {
    let cardsContainer = $('#cardsContainer');
    cardsContainer.empty();
    let row = $('<div class="row"></div>');


    // data.data.forEach(item => {
    //     if (item.isActive === isActive && item.inStock > 0) {



    //         let categoryList = item.categories[0].name;
    //             for (let category = 1; category < item.categories.length; category++) {
    //                 categoryList += "-" + item.categories[category].name;
    //             }

    //         const card = `
    //         <div class="col-md-3 col-md-3 col-md-3 mt-4">
    //             <div class="card mb-4 shadow-sm" style="width:22 rem; height:22rem">
    //                     <img src="${item.pictureUrl}" class="card-img-top" style="height:160px; width:286px" alt="">
    //                     <div class="card-body">
    //                         <p class="card-title h5 text-primary">${item.name}</p>


    //                         <p class="card-text">  ${categoryList}  </p>


    //                         <p class="card-title text-info">${item.price.toLocaleString("es-CR", { style: "currency", currency: "CRC" })}</p>
    //                         <div class="d-flex justify-content-between align-items-center">
    //                         <a href="../pages/Details.html?id=${item.id}" class="btn btn-primary stretched-link">View more</a>
    //                         </div>
    //                     </div>
    //             </div>        
    //         </div>
    //         `;

    //         row.append(card);
    //     }
    // });

    data.data.forEach(item => {
        if (item.isActive = isActive && item.inStock > 0) {
            let listItem = $('<div class="card bg-dark text-white mb-3" style="max-width: 18rem;"></div>');
            listItem.addClass('mb-3'); // Add margin class

            let image = $('<img src="' + item.pictureUrl + '" class="card-img-top" style="height:140px;width:220px" alt="">');
            let cardBody = $('<div class="card-body"></div>');
            let cardTitle = $('<h5 class="card-title">' + item.name + '</h5>');
            let categoryList = item.categories[0].name;

            for (let category = 1; category < item.categories.length; category++) {
                categoryList += "-" + item.categories[category].name;
            }
            let cardText = $('<p class="card-text">' + categoryList + '</p>');
            // let viewMoreLink = $('<a href="../pages/Details.html?id=' + item.id + '" class="btn btn-primary">View more</a>');
            let viewMoreLink = $(`<a href="../pages/Details.html?id=${item.id}" class="btn btn-primary stretched-link">View more</a>`);
            
            // Responsive classes for mobile and larger screens
            image.addClass('img-mobile'); // Mobile: full width
            cardTitle.addClass('card-title-mobile'); // Mobile: below the image

            // Append elements to the card body
            cardBody.append(image);
            cardBody.append(cardTitle);
            cardBody.append(cardText);
            cardBody.append(viewMoreLink);

            listItem.append(cardBody);

            row.append(listItem);
        }
    });


    cardsContainer.append(row);
}


function filterDataByProductCategories(data, searchTerm) {
    return data.data.filter(function (item) {
        // Filtra los datos según el término de búsqueda (ignorando mayúsculas y minúsculas)
        return item.categories[0].name.toLowerCase().includes(searchTerm.toLowerCase());
    });
}


$(document).ready(function () {
    $('#searchBar').on('input', function () {
        var filterValue = $(this).val().toLowerCase();

        // Ocultar todas las tarjetas
        $('.card').hide();

        // Mostrar solo las tarjetas que coinciden con el filtro
        $('.card').each(function () {
            var productName = $(this).find('.card-title').text().toLowerCase();
            var productCategories = $(this).find('.card-text').text().toLowerCase();

            if (productName.includes(filterValue)) {
                $(this).show();
            }

            if (productCategories.includes(filterValue)) {
                $(this).show();
            }
        });
    });
    fetchJsonData();
});


