;
; testTask.asm
;
; Created: 2/16/2019 1:24:43 AM
; Author : greg
;

.include "m128def.inc"

.cseg
.org 0000 ; Define start of Code segment

; Replace with your application code
start:
	; B as INPUT
	LDI R16, $FF ; Select Direction as Input on all pins
	OUT DDRB, R16 ; Set value in DDRB

	; A as Output
	LDI R16, $00 ; Select Direction as Output on all pins
	OUT DDRA, R16 ; Set value in DDRA
	LDI R16, $AA ; Set Initial value to high on all pins
	OUT PORTA, R16 ; Set PORTB value, Port B pins should be high

mainLoop:
	IN R16, PINB ; read B
	MOV r17, r16 ; copy
	MOV r18, r16 ; copy

	ANDI r17, $0F ; low part of B in r17
	ANDI r18, $F0 ; high part of B in r18*/

	CPI r17, $01
	BREQ case1 

	CPI r17, $02
	BREQ case2
	 
	CPI r17, $03
	BREQ case3

    rjmp mainLoop

	case1:
		IN  r20, PORTA 
		
		; looop 10 sec
		LDI r21, 10
			; spec says that atmega128 has 16Mhz speed
			Delay_1S:
				LDI r19, 49 
				Delay1: ; ~16kk
							
					LDI r18, 255 ; 1
					Delay2: ; 254 * (1279) + 1280 = 326144
						LDI r17, 255 ; 1
						Delay3: ; 254 * 5 ticks + 6 = 1276
							dec r17 ; 1
							nop		; 1
							nop		; 1
							nop		; 1
						brne Delay3 ; Two clock cycles for true 1 clock for false

					dec r18 ; 1
					brne Delay2 ; Two clock cycles for true 1 clock for false

				dec r19
				brne Delay1

			ROL r20
			OUT PORTA, r20
			DEC r21
			BRNE Delay_1S
		
	
		rjmp mainLoop

	case2: ; write to A high part of B (r18)
	    ; it is not clear how exactly, i decided to load them as low part
		swap r18
		out PORTA, r18
		
		rjmp mainLoop

	case3: ; invert A port
		IN  r16, PORTA 
		COM r16
		OUT PORTA, r16
	
		rjmp mainLoop
