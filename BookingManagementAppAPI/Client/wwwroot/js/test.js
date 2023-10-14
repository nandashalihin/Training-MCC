$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
    //success: (result) => {
    //    console.log(result);
    //}
}).done((result) => {
    console.log(result)
    let temp = "";
    $.each(result.results, (key, val) => {
        temp += `<tr>
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                    <td><button type="button" onclick="detail('${val.url}')" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalPoke">Detail</button></td>
                </tr >`;
    });
    $("#tbodyPoke").html(temp);


}).fail((error) => {
    console.log(error);
})

function createProgressBar(statName, baseStat) {
    // Menghitung persentase untuk progress bar
    const percentage = (baseStat / 255) * 100;

    // Mendefinisikan warna progress bar berdasarkan nama statistik
    let progressBarColorClass = "";
    if (statName === "hp") {
        progressBarColorClass = "progress-bar-hp";
    } else if (statName === "attack") {
        progressBarColorClass = "progress-bar-attack";
    } else if (statName === "defense") {
        progressBarColorClass = "progress-bar-defense";
    } else if (statName === "special-attack") {
        progressBarColorClass = "progress-bar-special-attack";
    } else if (statName === "special-defense") {
        progressBarColorClass = "progress-bar-special-defense";
    } else if (statName === "speed") {
        progressBarColorClass = "progress-bar-speed";
    }

    // Membuat elemen progress bar dengan teks dalam bar
    const progressBarHtml = `
        <div class="mb-2">
            <label class="d-flex justify-content-between">${statName} <span class="text-small">${baseStat}</span></label>
            <div class="progress">
                <div class="${progressBarColorClass}" role="progressbar" style="width: ${percentage}%" aria-valuenow="${baseStat}" aria-valuemin="0" aria-valuemax="255">
                </div>
            </div>
        </div>
    `;

    return progressBarHtml;
}

function detail(stringUrl) {
    $.ajax({
        url: stringUrl
    }).done((res) => {
        const moves = res.moves.map(abilityData => abilityData.move.name);
        const types = res.types;

        let typesString = "";

        types.forEach(typeData => {
            const typeName = typeData.type.name;
            typesString += `<span class="badge bg-primary me-2">${typeName}</span>`;
        });

        // Menggunakan URL gambar 'home' dan 'official-artwork' untuk Carousel
        const homeImageUrl = res.sprites.other.home.front_default;
        const officialArtworkImageUrl = res.sprites.other['official-artwork'].front_default;

        // Update elemen Carousel sesuai dengan data yang diterima dari API
        $("#pokemonImageCarousel .carousel-inner .carousel-item:nth-child(1) img").attr("src", homeImageUrl);
        $("#pokemonImageCarousel .carousel-inner .carousel-item:nth-child(2) img").attr("src", officialArtworkImageUrl);

        // Update elemen modal title dan jenis Pokemon
        $(".modal-title").html(res.name);
        $("#pokemonTypes").html(typesString);

        const stats = res.stats;

        let statsHtml = "";
        stats.forEach(statData => {
            const statName = statData.stat.name;
            const baseStat = statData.base_stat;
            statsHtml += createProgressBar(statName, baseStat);
        });

        $("#pokemonStats").html(statsHtml);

        const numberOfColumns = 3;
        const movesList = res.moves.map(moveData => moveData.move.name);

        const columnSize = Math.ceil(movesList.length / numberOfColumns);
        const movesColumns = Array.from({ length: numberOfColumns }, (_, i) => movesList.slice(i * columnSize, (i + 1) * columnSize));

        let movesListHtml = "";
        movesColumns.forEach(moves => {
            const movesHtml = moves.map(move => `<li>${move}</li>`).join("");
            movesListHtml += `<ul class="col">${movesHtml}</ul>`;
        });

        $("#pokemonAbilities").html(movesListHtml);
    });
}


