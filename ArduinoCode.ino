#include <Servo.h>
#include <Wire.h> 
#include <LiquidCrystal_I2C.h>
#include <Buzzer.h>

Servo myservo;
int message = 0;
int pos = 0; 
LiquidCrystal_I2C lcd(0x27,20,4);
String pin = "";
Buzzer buzzer(10, 13);

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  myservo.attach(9);
  myservo.write(0);
  lcd.init();
  lcd.backlight();
  lcdTemplate();
  pinMode(10,OUTPUT);
}

void lcdTemplate()
{
  pin = "";
  lcd.clear(); 
  lcd.setCursor(3,0);
  lcd.print("Twoj Pin:");
  lcd.setCursor(2,1);
  lcd.print(pin);
}

void lcdTemplate(char nextChar)
{
  lcd.clear();
  pin = pin + " " + nextChar; 
  lcd.setCursor(3,0);
  lcd.print("Twoj Pin:");
  lcd.setCursor(2,1);
  lcd.print(pin);
}

void buzzerMelody()
{
  buzzer.begin(100);
  buzzer.sound(NOTE_E7, 80);
  buzzer.sound(NOTE_E7, 80);
  buzzer.sound(0, 80);
  buzzer.sound(NOTE_E7, 80);
  buzzer.sound(0, 80);
  buzzer.sound(NOTE_C7, 80);
  buzzer.sound(NOTE_E7, 80);
  buzzer.sound(0, 80);
  buzzer.sound(NOTE_G7, 80);
  buzzer.sound(0, 240);
  buzzer.sound(NOTE_G6, 80);
}

void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available() > 0)
  {
    message = Serial.read();
    Serial.print("Recieved: ");
    Serial.println((char)message);
    if((char)message == 'b')
    {
      for (pos = 0; pos <= 130; pos += 1) 
      { 
        myservo.write(pos);              
        delay(10);                       
      }
    }
    else if((char)message == 'a')
    {
      buzzerMelody();
      lcd.clear();
      lcd.setCursor(5,0);
      lcd.print("Sukces");
      lcd.setCursor(3,1);
      lcd.print("Otwieranie");
      
      for (pos = 130; pos >= 0; pos -= 1) 
      {
        myservo.write(pos);              
        delay(10);                      
      }  
    }
    else if((char)message == 'e')
    {
      lcd.clear();
      lcd.setCursor(0,0);
      lcd.print("Nie poprawny pin");
      digitalWrite(10,HIGH);
      delay(900);
      digitalWrite(10,LOW);

      //otwieranie
      for (pos = 130; pos >= 55; pos -= 1) 
      { 
          myservo.write(pos);              
          delay(10);                       
      }

      delay(650);

      //zamykanie
      for (pos = 55; pos <= 130; pos += 1) 
      { 
        myservo.write(pos);              
        delay(10);                       
      }
      lcdTemplate();

    }
    else if((char)message == 'c')
    {
      lcdTemplate();
    }
    else if (message !=10)
    {
      lcdTemplate((char)message);
      digitalWrite(10,HIGH);
      delay(100);
      digitalWrite(10,LOW);
    }
  }
}
