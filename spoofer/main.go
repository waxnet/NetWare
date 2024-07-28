package main

// libraries
import (
	"spoofer/utilities/console"
	"spoofer/utilities/network"
	"spoofer/utilities/registry"
)

func main() {
	// console setup
	console.SetSize(80, 20)
	console.SetTitle("NetWare Spoofer")

	for {
		// display banner
		console.Clear()
		console.DisplayBanner()

		selection := console.Inp(" [1] Spoof\n [2] Exit\n\n", "gray")

		// display banner without options
		console.Clear()
		console.DisplayBanner()

		switch selection {
		case '1':
			spoof()
		case '2':
			return
		}
	}
}

// methods
func spoof() {
	// start
	console.Out("Spoofing . . .", "yellow")

	// generate new account
	refreshToken, status := network.GenerateAccount()
	if !status {
		console.Err("000")
		return
	}

	// set refresh token
	refreshTokenKey := registry.FindRefreshTokenKey()
	if refreshTokenKey == "" {
		console.Err("001")
		return
	}
	if status := registry.SetRefreshToken(refreshTokenKey, refreshToken); !status {
		console.Err("002")
		return
	}

	// set sign in platform
	signInPlatformKey := registry.FindSignInPlatformKey()
	if signInPlatformKey == "" {
		console.Err("003")
		return
	}
	if status := registry.SetSignInPlatform(signInPlatformKey); !status {
		console.Err("004")
		return
	}

	// end
	console.Out("Done!", "green")
	console.Inp("\nPress any key to continue . . .", "gray")
}
