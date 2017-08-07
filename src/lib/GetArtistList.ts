import * as https from "https";

export default function(artistName : string) {
    const baseArtistSearchUrl = "https://musicbrainz.org/ws/2/artist?query=";
    const userAgent = "Musichino_Bot/0.1 ( https://github.com/vitorussomoliterni/musichino/ )";
    let rawData = '';

    const options = {
        hostname: "musicbrainz.org",
        path: "/ws/2/artist?query=" + artistName,
        method: "GET",
        headers: {
            "User-Agent": userAgent
        }
    };
    
    https.get(options, (res) => {
        const { statusCode } = res;
        const contentType = res.headers['content-type'];
        res.on('data', (chunk) => { rawData += chunk; });
        res.on('end', ()=> {
            console.log(rawData); 
        })
    }).on('error', (e) => {
        console.error(`Got error: ${e.message}`);
    });

    return rawData; 
}