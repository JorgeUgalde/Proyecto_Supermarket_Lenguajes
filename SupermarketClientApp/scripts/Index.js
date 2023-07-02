
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


    data.data.forEach(item => {
        if (item.isActive === isActive && item.inStock > 0) {

            const card = `
            <div class="col-md-3 col-md-3 col-md-3 mt-4">
                <div class="card mb-4 shadow-sm" style="width:18rem; height:18rem">
                        <img src="${item.pictureUrl}" class="card-img-top" style="height:160px; width:286px" alt="">
                        <div class="card-body">
                            <p class="card-title h5 text-primary">${item.name}</p>
                            <p class="card-title text-info">${item.price}</p>
                            <div class="d-flex justify-content-between align-items-center">
                            <a href="../pages/Details.html?id=${item.id}" class="btn btn-primary stretched-link">View more</a>
                            </div>
                        </div>
                </div>        
            </div>
            `;

            const card2 = `
            <div class='container-fluid'>
        <div class="card mx-auto col-md-3 col-10 mt-5">
            <img class='mx-auto img-thumbnail'
                src="${item.pictureUrl}"
                width="auto" height="auto"/>
            <div class="card-body text-center mx-auto">
                <div class='cvp'>
                    <h5 class="card-title font-weight-bold">${item.name}</h5>
                    <p class="card-text">${item.price}</p>
                    <a href="../pages/Details.html?id=${item.id}" class="btn details px-auto">View More</a><br />
                </div>
            </div>
        </div>
        </div>
            `;


            // let listItem = $('<div class="card bg-dark text-white mb-3" style="max-width: 18rem;"></div>');
            // listItem.addClass('mb-3'); // Add margin class

            // let image = $('<img src="' + item.pictureUrl + '" class="card-img-top" style="height:140px;width:220px" alt="">');
            // let cardBody = $('<div class="card-body"></div>');
            // let cardTitle = $('<h5 class="card-title">' + item.name + '</h5>');
            // let cardText = $('<p class="card-text">fuap</p>');
            // let viewMoreLink = $('<a href="../pages/Details.html?id=' + item.id + '" class="btn btn-primary">View more</a>');

            // // Store product data in local storage when the user clicks on the "View more" button
            // // viewMoreLink.on('click', function() {
            // //     localStorage.setItem('productData', JSON.stringify(item));
            // // });

            // // Responsive classes for mobile and larger screens
            // image.addClass('img-mobile'); // Mobile: full width
            // cardTitle.addClass('card-title-mobile'); // Mobile: below the image

            // // Append elements to the card body
            // cardBody.append(image);
            // cardBody.append(cardTitle);
            // cardBody.append(cardText);
            // cardBody.append(viewMoreLink);

            // listItem.append(cardBody);

            row.append(card);
        }
    });

    cardsContainer.append(row);
}


function filterDataByProductName(data, searchTerm) {
    return data.data.filter(function (item) {
        // Filtra los datos según el término de búsqueda (ignorando mayúsculas y minúsculas)
        return item.name.toLowerCase().includes(searchTerm.toLowerCase());
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

            if (productName.includes(filterValue)) {
                $(this).show();
            }
        });
    });
    fetchJsonData();
});
