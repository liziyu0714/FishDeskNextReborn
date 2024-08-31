.PHONY: clean

fdnr:
	mkdir -p src/FishDeskNextReborn/bin
	mkdir -p src/FDNRBox/bin
	dotnet publish

clean:
	rm -rf src/FishDeskNextReborn/bin/*
	rm -rf src/FDNRBox/bin/*