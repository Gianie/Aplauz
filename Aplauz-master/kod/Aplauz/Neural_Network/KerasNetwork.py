from keras.models import Sequential
import numpy as np
from keras.layers import Dense, Activation, Flatten
from keras.models import load_model
import os


import keras
from keras.utils.np_utils import to_categorical



model = Sequential()
model.add(Dense(32,input_shape=(1,146)))
model.add(Activation('relu'))

model.add(Flatten())

model.add(Dense(1))


model.compile(optimizer='adam',
              loss='mean_squared_error',
              metrics=['accuracy'])


#TRAINING
for filename in os.listdir('C:/Dev/Trening'):
    csv = np.genfromtxt('C:/Dev/Trening/'+filename, delimiter=",")
    print(filename)
    number_of_rows=csv.shape[0]


    stan_gry=csv[:,:146]
    oceny=csv[:,146]
    trainX=stan_gry
    trainY=oceny
    #trainX=stan_gry[0:100]
    #trainY=oceny[0:100]


    shape=trainX.shape

    shape2=trainY.shape


    #testX=stan_gry[100:]
    #testY=oceny[100:]



    trainX=trainX.reshape(trainX.shape[0],1,146)
    #testX=testX.reshape(testX.shape[0],1,146)


    trainX = trainX.astype('float32')
    #testX = testX.astype('float32')
    trainY = trainY.astype('float32')
    #testY = testY.astype('float32')

    # trainY =to_categorical(trainY,16)
    # testY =to_categorical(testY,16)

    #trainY=to_categorical(trainY)
    print(stan_gry)
    print(stan_gry.size)




    model.fit(trainX, trainY, epochs=5, batch_size=10)


#loss_and_metrics = model.evaluate(testX, testY, batch_size=300)
#a=model.predict(testX,batch_size=52)

#print(a)
najwyzszy=0
najwyzszy_index=0


model.save('my_model.h5')  # creates a HDF5 file 'my_model.h5'
del model  # deletes the existing model

# returns a compiled model
# identical to the previous one
model = load_model('my_model.h5')
a=model.predict(trainX,batch_size=52)
print(a)
# for v in a:
#
#     for b in v:
#         if b>najwyzszy:
#             najwyzszy_index=np.where(v==b)
#             najwyzszy=b
#
#     print(najwyzszy)
#     print(najwyzszy_index)
#     najwyzszy_index=0
#     najwyzszy=0
#     print("dsfgsdfsdf")

#print(a)
#print(testY)

#model.summary()





#
# from keras.models import Sequential
# import numpy as np
# from keras.layers import Dense, Activation, Flatten
# import keras
# from keras.utils.np_utils import to_categorical
#
#
# csv = np.genfromtxt ('E:/Studia/state.csv', delimiter=",")
#
# #print(csv)
# stan_gry=csv[:,:146]
# oceny=csv[:,146]
#
# trainX=stan_gry[0:100]
# trainY=oceny[0:100]
#
#
# shape=trainX.shape
#
# shape2=trainY.shape
#
#
# testX=stan_gry[100:]
# testY=oceny[100:]
#
#
#
# trainX=trainX.reshape(trainX.shape[0],1,146)
# testX=testX.reshape(testX.shape[0],1,146)
#
#
# trainX = trainX.astype('float32')
# testX = testX.astype('float32')
#
#
# trainY =to_categorical(trainY,16)
# testY =to_categorical(testY,16)
#
# #trainY=to_categorical(trainY)
# print(stan_gry)
# print(stan_gry.size)
#
# #
# model = Sequential()
# model.add(Dense(32,input_shape=(1,146)))
# model.add(Activation('relu'))
#
# model.add(Flatten())
#
# model.add(Dense(16))
# model.add(Activation('softmax'))
#
# model.compile(optimizer='rmsprop',
#               loss='categorical_crossentropy',
#               metrics=['accuracy'])
#
#
# model.fit(trainX, trainY, epochs=5, batch_size=10)
#
# #loss_and_metrics = model.evaluate(testX, testY, batch_size=300)
# a=model.predict(testX,batch_size=52)
#
# #print(a)
# najwyzszy=0
# najwyzszy_index=0
#
#
# # for v in a:
# #
# #     for b in v:
# #         if b>najwyzszy:
# #             najwyzszy_index=np.where(v==b)
# #             najwyzszy=b
# #
# #     print(najwyzszy)
# #     print(najwyzszy_index)
# #     najwyzszy_index=0
# #     najwyzszy=0
# #     print("dsfgsdfsdf")
#
# print(a)
#
#
# model.summary()

