async function fetchProducts(categoryId) {
    try {
        const response = await fetch(`/Home/RelatedProducts?categoryId=${categoryId}`);
        const html = await response.text(); 

        document.querySelector('.product-slider').innerHTML = html;

    } catch (error) {
        console.error('Error fetching products:', error);
    }
}
async function addToBasket(id) {
    try {
        const response = await fetch(`/Home/AddToBasket?productId=${id}`);
        const html = await response.text(); 

        document.querySelector('.cart-dropdown-block').innerHTML += html;
    }
    catch (error) {
        console.error('Error fetching products:', error);

    }
}