El proyecto por defecto tiene tres clases; una clase Moogle donde voy a implementar lo necesario para
que el programa busque y dos clases que me ayudan a organizar esos resultados de la búsqueda .La
primera es searchitem para caracterizar a un resultado de la búsqueda la misma tiene tres
propiedades ,el title snippet score esta última es que tanto se parece a lo que estoy buscando y es lo que 
utilizo para ordenar los resultados en relación a la concordancia que tienen o la similitud que tienen con
la query , el snippet es un fragmento que voy a devolver junto con ese resultado mostrando que ese
resultado que devuelve tiene realmente que ver con mi query, el title es el título del libro que estoy
devolviendo.
Para este proyecto implemente las siguentes clases comenzando por la clase Book  para guardar todo lo
referente a la información necesaria para la búsqueda de un libro o sea todo lo que se necesita sacar de
un libro, clase Library será mi biblioteca para guardar la información de varios libros para tener más
información creando así un conjunto de documentos más organizados, clase toolsBox que contiene
métodos útiles que se usarán más adelante para poder realizar la búsqueda estos códigos son
generalmente recurrente para usarlos varias veces llamándolos desde cualquier otro lugar desde
cualquier clase, SearchEngine este sería el motor de busqueda para construir la biblioteca para poder
tener toda la información almacenada aquí se realizan todos los procesos para poder realizar la
búsqueda es donde también esta implementado el modelo vectorial utilizado para poder dar los
resultados de acuerdo a la búsqueda y en el método search se construyen los resultados de búsqueda,
método que llama a todos los demás métodos ya sea de las demás clases y de la propia clase en sí para
poder construir el resultado que queremos mandar, devolviendo un serchresult que en la clase Moogle
simplemente tenemos que generar una instancia de la clase SearchEngine iniciarla y preguntarle a esa
instancia por los resultados de la búsqueda, a través del método Search.