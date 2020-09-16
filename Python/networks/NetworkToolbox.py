import numpy as np
import scipy.special as sp

class CreateNetwork():
    count = 0

    def Create(structure, name = "Network" + str(count)):
        CreateNetwork.count += 1
        return type(name, (NetworkParent,), {"structure": np.asarray(structure), "count": 0})

class NetworkParent():
    @property
    def weights(self):
        return self.__weights
    @property
    def bias(self):
        return self.__bias
    @property
    def outs(self):
        return self.__outs
    @property
    def error(self):
        return self.__error
    @property
    def SumErrors(self):
        return self.__SumErrors
    @property
    def struture(self):
        return self.__struture


    def __init__(self):
        self.__weights = np.arange(self.structure.size-1, dtype = object)
        self.__bias = np.arange(self.structure.size-1, dtype = object)
        self.__outs = np.arange(self.structure.size, dtype = object)
        self.__deltas = np.arange(self.structure.size, dtype = object)
        self.__error = 0
        self.__SumErrors = 0
        self.count += 1

    def __del__(self):
        self.count -= 1


    def initial(self, Mx = 0, Dx = 1):
        Dx = 1/np.power(self.structure[:-1]+1, 0.5)
        for i in range(1, self.structure.size):
            self.__weights[i-1] = np.random.normal(Mx, Dx[i-1], size = (self.structure[i-1], self.structure[i]))
            self.__bias[i-1] = np.random.normal(Mx, Dx[i-1], size = (1, self.structure[i]))

    def forward_propagation(self, input_data, output_data, getPrediction = False):
        input_data = np.asarray(input_data)
        self.__outs[0] = input_data
        for i in range(1, self.structure.size):
            if i == self.structure.size-1:
                self.__outs[i] = np.dot(self.__outs[i-1], self.__weights[i-1]) + self.__bias[i-1]
            else:
                self.__outs[i] = sp.expit( np.dot(self.__outs[i-1], self.__weights[i-1]) + self.__bias[i-1])
        if getPrediction == False:
            output_data = np.asarray(output_data)
            self.__error = output_data - self.__outs[self.structure.size - 1]
            self.__SumErrors += self.__error*self.__error

    def back_propagation(self):
        self.deltas[self.structure.size - 1] = self.error
        for i in range(self.structure.size-2, 0, -1):
            self.deltas[i] = np.dot(self.deltas[i+1], self.__weights[i].T) * self.__outs[i]*(1 - self.__outs[i])

    def ajustments(self, learn_rate = 0.1):
        for i in range(self.structure.size-1):
            self.__weights[i] += learn_rate * np.dot(self.__outs[i].T, self.deltas[i+1])
            self.__bias[i] += learn_rate * self.deltas[i+1]

    def learning(self, input_data, output_data):
        self.forward_propagation(input_data, output_data)
        self.back_propagation()
        self.ajustments()

    def mutation(self, degree = 0.005):
        for numWeight in range(len(self.__weights)):
            for i in range(self.__weights[numWeight].shape[0]):
                for j in range(self.__weights[numWeight].shape[1]):
                    if np.random.random() <= (degree):
                        self.__weights[numWeight][i][j] += np.random.normal(0, 1)
            for l in range(self.__bias[numWeight].size):
                if np.random.random() <= (degree):
                    self.__bias[numWeight][0][l] += np.random.normal(0, 1)

    def crossWith(self, otherNetwork):
        childNet = self.__class__()
        for numWeight in range(len(self.__weights)):
            condition = np.random.randint(2, size = self.__weights[numWeight].shape, dtype = bool)
            childNet.__weights[numWeight] = np.where(condition, self.__weights[numWeight], otherNetwork.__weights[numWeight])
            condition = np.random.randint(2, size = self.__bias[numWeight].shape, dtype = bool)
            childNet.__bias[numWeight] = np.where(condition, self.__bias[numWeight], otherNetwork.__bias[numWeight])
        return childNet

    def getPredictFor(self, DataSet):
        for i in range(len(DataSet)):
            Net.forward_propagation(DataSet[i], y[i])
            ans = Net.outs[- 1]
            y1.append(ans[0][0])
        return y

class Generation():
    
    def newGeneration(quantityNetworks = 10):
        listOfNetworks = list(range(10))
        for numberNet in range(quantityNetworks):
            Net = Network(structure)
            Net.initial()
            listOfNetworks[numberNet] = Net
        return listOfNetworks

    def crossing(listOfNetworks, probabilityMutation = 1.0, degreeMutation = 0.01):
        newNetworksGen = np.arange(0, dtype = object)
        childNum = 0
        motherNum = 1
        for father in listOfNetworks[0:-1]:
            for mother in listOfNetworks[motherNum:]:
                newNetworksGen = np.append(newNetworksGen, father.crossWith(mother))
                if np.random.random() <= probabilityMutation:
                    newNetworksGen[childNum].mutation(degreeMutation)
                childNum += 1
            motherNum += 1
        return newNetworksGen