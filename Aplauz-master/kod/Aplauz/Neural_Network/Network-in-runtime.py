from keras.models import Sequential
import numpy as np
import sys
from keras.layers import Dense, Activation, Flatten
from keras.models import load_model
import os


ruchy = sys.argv[1].split(',')

stanGry = sys.argv[2].split(',')

mozliwe_ruchy=np.array(ruchy,dtype=float)
stan_gry=np.array(stanGry,dtype=float)

stan_gry=stan_gry.astype('float32')
mozliwe_ruchy=mozliwe_ruchy.astype('float32')




#mozliwe_ruchy=np.genfromtxt('C:/Dev/possible_moves/possible_moves.txt', delimiter=",")
#state = np.genfromtxt('C:/Dev/Exports_50_RandomPlayers/16112017145526.csv', delimiter=",")
#stan=state[0]
#stan_gry=stan[0:145]
#print(stan_gry)
model = load_model('my_model.h5')
print(mozliwe_ruchy)
print("ugabuga")

najlepszy=0
# testX=stan_gry
# np.append(testX,mozliwe_ruchy[0])
# print(testX)
for a in mozliwe_ruchy:
    tmp=stan_gry
    tmp=np.append(tmp,a)
    testX=tmp
    # a = tmp.shape
    # print(a)
    # tmp=tmp[0]
    # #print(testX)
    # testX=tmp
    # a=testX.shape
    # print("shape to "+str(a))
    testX=testX.reshape(1,1,146)
    testX = testX.astype('float32')
    b = model.predict(testX, batch_size=1)
    print("wynik:")
    print(b)
    if b[0]>najlepszy:
        najlepszy=a

print("Wybierz ruch: "+str(najlepszy))



