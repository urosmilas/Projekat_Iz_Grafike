Implemtirani su Framebufferi, i preko njih filteri za slepe za boje, implentiran je ugradjeni MSAA sa 4 tacke, kao i HDR i Bloom, zajedno sa obaveznim lekcijama.

Preko F1 mozete da udjete u GUI i da testirate osvetljenje, bloom (B za ON/OFF, Q i E za menjanje ekspoziture), i filtere za slepe za boje (1, 2 i 3 za ON/OFF i 0 za OFF).

Dodat je video u repozitorijumu.

Slikama se u fragment shaderu ne uracunava svetlost jer bi to i u najboljem slucaju postavljanja svetala narusuvalo vidljivost detalja boja, a posto je poenta slika da se vide filteri za boje onda to ne bi imalo smisla. Za druge stvari je svetlost uracunata naravno.
