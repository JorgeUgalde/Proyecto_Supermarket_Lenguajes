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

    function generateCards(data) {
        let cardsContainer = $('#cardsContainer');
        cardsContainer.empty();
        let row = $('<div class="row"></div>');
    
        data.data.forEach(item => {
            let listItem = $('<div class="card bg-dark text-white mb-3" style="max-width: 18rem;"></div>');
            listItem.addClass('mb-3'); // Add margin class
            
            let image = $('<img src="' + item.pictureUrl + '" class="card-img-top" alt="">');
            let cardBody = $('<div class="card-body"></div>');
            let cardTitle = $('<h5 class="card-title">' + item.name + '</h5>');
            let cardText = $('<p class="card-text">fuap</p>');
            let viewMoreLink = $('<a href="#" class="btn btn-primary">View more</a>');
    
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
        });
    
        cardsContainer.append(row);
    }

    $( document ).ready(function() {
        fetchJsonData();
    });
