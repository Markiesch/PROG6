const nameInput = document.getElementById('name');
const typeInput = document.getElementById('type');
const priceInput = document.getElementById('price');
const imageInput = document.getElementById('image');

const animalName = document.getElementById('animal-name');
const animalType = document.getElementById('animal-type');
const animalPrice = document.getElementById('animal-price');
const animalImage = document.getElementById('animal-image');

nameInput.addEventListener('input', updateAnimal);
typeInput.addEventListener('change', updateAnimal);
priceInput.addEventListener('input', updateAnimal);
imageInput.addEventListener('input', updateAnimal);

function updateAnimal() {
    animalName.innerText = nameInput.value;
    animalType.innerText = typeInput.value;
    animalPrice.innerText = priceInput.value;
    animalImage.src = imageInput.value;
}

updateAnimal();
