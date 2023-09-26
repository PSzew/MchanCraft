#include <Servo.h>
Servo myservo;
int message = 0;
int pos = 0; 

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  myservo.attach(9);
}

void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available() > 0)
  {
    message = Serial.read();
    
    Serial.print("Recieved: ");
    Serial.println((char)message);
    if((char)message == 'a')
    {
      for (pos = 0; pos <= 180; pos += 1) 
      { 
        myservo.write(pos);              
        delay(15);                       
      }
    }
    else if((char)message == 'b')
    {
      for (pos = 180; pos >= 0; pos -= 1) 
      { 
          myservo.write(pos);              
          delay(15);                       
        }  
    }
  }
}
