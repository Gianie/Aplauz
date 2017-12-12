from keras.models import Sequential
import numpy as np
from keras.layers import Dense, Activation, Flatten
from keras.models import load_model
import os


model = Sequential()
model.add(Dense(32,input_shape=(1,146)))
model.add(Activation('relu'))

model.add(Flatten())
model.add(Activation('softmax'))
model.add(Dense(1))


model.compile(optimizer='adam',
              loss='mean_squared_error',
              metrics=['accuracy'])


#TRAINING
for filename in os.listdir('C:/Dev/TreningPoprawneDlugieStany'):
    csv = np.genfromtxt('C:/Dev/TreningPoprawneDlugieStany/'+filename, delimiter=",")
    print(filename)
    number_of_rows=csv.shape[0]


    stan_gry=csv[:,:146]
    oceny=csv[:,146]
    for a in range(len(oceny)):
        oceny[a]=oceny[a]*oceny[a]*oceny[a]*oceny[a]*oceny[a]*oceny[a]
    trainX=stan_gry
    trainY=oceny

    shape=trainX.shape

    shape2=trainY.shape

    trainX=trainX.reshape(trainX.shape[0],1,146)

    trainX = trainX.astype('float32')
    trainY = trainY.astype('float32')

    print(stan_gry)
    print(stan_gry.size)




    model.fit(trainX, trainY, epochs=5, batch_size=10)

najwyzszy=0
najwyzszy_index=0


model.save('modelDlugieStanyPoprawne.h5')
del model


model = load_model('modelDlugieStanyPoprawne.h5')
a=model.predict(trainX,batch_size=52)
print(a)

