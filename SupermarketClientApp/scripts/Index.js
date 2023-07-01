
const isActive = 1;

function fetchJsonData() {
    $.ajax({
        url: 'http://proyectoapps-001-site1.atempurl.com/Customer/Home/GetAll',
        type: 'GET',
        success: function(data) {
            generateCards(data);
        }
    });
    }

    // function generateCards(data) {
    //     let cardsContainer = $('#cardsContainer');
    //     cardsContainer.empty();
    //     data.data.forEach(item => {
    //       let listItem = $('<div class="card text-bg-dark mb-3" style="max-width: 18rem;"></div>');
    //       listItem.append('<img src="' + item.pictureUrl + '" class="card-img-top" alt="">');
    //       let cardBody = $('<div class="card-body"></div>');
    //       cardBody.append('<h5 class="card-title">' + item.name + '</h5>');
    //       cardBody.append('<p class="card-text">Some quick example text to build on the card title and make up the bulk of the card content.</p>');
    //       cardBody.append('<a href="#" class="btn btn-primary">View more</a>');
    //       listItem.append(cardBody);
    //       cardsContainer.append(listItem);
    //     });
    // }

    // function generateCards(data) {
    //     let cardsContainer = $('#cardsContainer');
    //     cardsContainer.empty();
    //     let row = $('<div class="row"></div>');
    
    //     data.data.forEach(item => {
    //         let listItem = $('<div class="card bg-dark text-white mb-3" style="max-width: 18rem;"></div>');
    //         listItem.addClass('mb-3'); // Add margin class
            
    //         let image = $('<img src="' + item.pictureUrl + '" class="card-img-top" alt="">');
    //         let cardBody = $('<div class="card-body"></div>');
    //         let cardTitle = $('<h5 class="card-title">' + item.name + '</h5>');
    //         let cardText = $('<p class="card-text">fuap</p>');
    //         // let viewMoreLink = $('<a href="#" class="btn btn-primary">View more</a>');
    //         let viewMoreLink = $('<a href="../pages/Details.html?id=' + item.id + '" class="btn btn-primary">View more</a>');

    //         // Responsive classes for mobile and larger screens
    //         image.addClass('img-mobile'); // Mobile: full width
    //         cardTitle.addClass('card-title-mobile'); // Mobile: below the image
    
    //         // Append elements to the card body
    //         cardBody.append(image);
    //         cardBody.append(cardTitle);
    //         cardBody.append(cardText);
    //         cardBody.append(viewMoreLink);
            
    //         listItem.append(cardBody);
    
    //         row.append(listItem);
    //     });
    
    //     cardsContainer.append(row);
    // }




    function generateCards(data) {
        let cardsContainer = $('#cardsContainer');
        cardsContainer.empty();
        let row = $('<div class="row"></div>');
    
        
        data.data.forEach(item => {
            if(item.isActive = isActive && item.inStock > 0) {

            
                let listItem = $('<div class="card bg-dark text-white mb-3" style="max-width: 18rem;"></div>');
                listItem.addClass('mb-3'); // Add margin class
    
                let image = $('<img src="' + item.pictureUrl + '" class="card-img-top" style="height:140px;width:220px" alt="">');
                let cardBody = $('<div class="card-body"></div>');
                let cardTitle = $('<h5 class="card-title">' + item.name + '</h5>');
                let cardText = $('<p class="card-text">fuap</p>');
                let viewMoreLink = $('<a href="../pages/Details.html?id=' + item.id + '" class="btn btn-primary">View more</a>');
    
                // Store product data in local storage when the user clicks on the "View more" button
                viewMoreLink.on('click', function() {
                    localStorage.setItem('productData', JSON.stringify(item));
                });
    
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
    

    function filterDataByProductName(data, searchTerm) {
        return data.data.filter(function(item) {
            // Filtra los datos según el término de búsqueda (ignorando mayúsculas y minúsculas)
            return item.name.toLowerCase().includes(searchTerm.toLowerCase());
        });
    }
    

    $( document ).ready(function() {
        $('#searchBar').on('input', function() {
            var filterValue = $(this).val().toLowerCase();
    
            // Ocultar todas las tarjetas
            $('.card').hide();
    
            // Mostrar solo las tarjetas que coinciden con el filtro
            $('.card').each(function() {
                var productName = $(this).find('.card-title').text().toLowerCase();
    
                if (productName.includes(filterValue)) {
                    $(this).show();
                }
            });
        });
        fetchJsonData();
    });
