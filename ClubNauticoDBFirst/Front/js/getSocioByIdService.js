let id = document.getElementById('txtId')

const API_SOCIOS_URL = 'https://localhost:7205/api/Socio/getById/'  // link de obtener todas las personas / importanate

function listaSocioById() {
    fetch(API_SOCIOS_URL + id.value) //le paso el valor del id par que busque
    
        .then((respuesta) => respuesta.json())
        .then((respuesta) => {
            if (!respuesta.ok) {
                alert("ERROR!")
                return
            }

            const cuerpoTabla = document.querySelector('tbody')
           
            cuerpoTabla.innerHTML = ''; //vacio la lista
            
            // nombre de la lista en swagger
            respuesta.listSocios.forEach((soc) => {
                const fila = document.createElement('tr')
                fila.innerHTML += `<td>${soc.id}</td>` // filas de la lista
                fila.innerHTML += `<td>${soc.nombre}</td>`
                fila.innerHTML += `<td>${soc.apellido}</td>`
                fila.innerHTML += `<td>${soc.telefono}</td>`

                cuerpoTabla.append(fila)
            });


        }).catch((err)=>{
            alert("No funciono")
        })
    

}